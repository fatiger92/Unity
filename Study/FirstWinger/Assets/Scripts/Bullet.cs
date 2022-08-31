using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OwnerSide
{
    Player = 0,
    Enemy
}

public class Bullet : MonoBehaviour
{
    private const float LifeTime = 15f;     // 총알의 생존 시간
    
    [SerializeField] private Vector3 MoveDirection = Vector3.zero;
    [SerializeField] private float Speed = 0f;
    
    private bool NeedMove = false;      // 이동 플래그

    private float FiredTime;
    private bool Hited = false;         // 부딪혔는지 플래그

    [SerializeField] private int Damage = 1;

    private Actor Owner;

    public string FilePath { set; get; }

    void Update()
    {
        if (ProcessDisappearCondition())
            return;
        
        UpdateMove();
    }

    private void UpdateMove()
    {
        if (!NeedMove)
            return;

        Vector3 moveVector = MoveDirection.normalized * (Speed * Time.deltaTime);
        moveVector = AdjustMove(moveVector);
        transform.position += moveVector;
    }

    public void Fire(Actor owner, Vector3 firePosition, Vector3 direction, float speed, int damage)
    {
        Owner = owner;
        transform.position = firePosition;
        MoveDirection = direction;
        Speed = speed;
        Damage = damage;
        
        NeedMove = true;
        FiredTime = Time.time;
    }

    private Vector3 AdjustMove(Vector3 moveVector)
    {
        RaycastHit hitInfo;
       
        if (Physics.Linecast(transform.position, transform.position + moveVector, out hitInfo))
        {
            moveVector = hitInfo.point - transform.position;
            OnBulletCollision(hitInfo.collider);
        }

        return moveVector;
    }

    private void OnBulletCollision(Collider collider)
    {
        if (Hited)
            return;

        if (collider.gameObject.layer == LayerMask.NameToLayer("EnemyBullet")
            || collider.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
            return;

        Actor actor = collider.GetComponentInParent<Actor>();
        if (actor && actor.IsDead)
            return;
        
        actor.OnBulletHited(Owner, Damage);
        
        // 충돌 시 더 이상 충돌 감지를 하지 않도록 꺼줌 - 이건 좋은 방법인듯
        Collider myCollider = GetComponentInChildren<Collider>();
        myCollider.enabled = false;
        
        Hited = true;
        NeedMove = false;
        
        GameObject go = SystemManager.Instance.EffectManager.GenerateEffect(EffectManager.BulletDisappearFxIndex, transform.position);
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Disappear();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnBulletCollision(other);
    }

    private bool ProcessDisappearCondition()
    {
        if (transform.position is {x: > 15f or < -15f} or {y: > 15f or < -15F})
        {
            Disappear();
            return true;
        }
        
        if (Time.time - FiredTime > LifeTime)
        {
            Disappear();
            return true;
        }
        
        return false;
    }

    private void Disappear()
    {
        SystemManager.Instance.BulletManager.Remove(this);
    }
}
