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
    private const float LifeTime = 3f;
    
    private OwnerSide ownerSide = OwnerSide.Player;
    
    [SerializeField] private Vector3 MoveDirection = Vector3.zero;
    [SerializeField] private float Speed = 0f;
    
    private bool NeedMove = false;

    private float FiredTime;
    private bool Hited = false;

    [SerializeField] private int Damage;
    
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

    public void Fire(OwnerSide FireOwner, Vector3 firePosition, Vector3 direction, float speed, int damage)
    {
        ownerSide = FireOwner;
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
        
        if (ownerSide == OwnerSide.Player)
        {
            var enemy = collider.GetComponentInParent<Enemy>();
            
            if (enemy.IsDead)
                return;

            enemy.OnBulletHited(Damage);
        }
        else
        {
            var player = collider.GetComponentInParent<Player>();
            
            if (player.IsDead)
                return;

            player.OnBulletHited(Damage);
        }
        
        // 충돌 시 더 이상 충돌 감지를 하지 않도록 꺼줌 - 이건 좋은 방법인듯
        Collider myCollider = GetComponentInChildren<Collider>();
        myCollider.enabled = false;
        
        Hited = true;
        NeedMove = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnBulletCollision(other);
    }

    private bool ProcessDisappearCondition()
    {
        if (transform.position is {x: > 15f or < -15f} or {y: > 15f or < -15F})
        {
            Disapear();
            return true;
        }
        
        if (Time.time - FiredTime > LifeTime)
        {
            Disapear();
            return true;
        }
        
        return false;
    }

    private void Disapear()
    {
        Destroy(gameObject);
    }
}
