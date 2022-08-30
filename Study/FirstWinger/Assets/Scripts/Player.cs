using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private Vector3 MoveVector = Vector3.zero;
    [SerializeField] private float Speed;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Transform MainBGQuadTransform;

    [SerializeField] private Transform FireTransform;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float BulletSpeed = 1f;

    protected override void UpdateActor()
    {
        UpdateMove();
    }
    
    private void UpdateMove()
    {
        // sqrMagnitude는 루트 연산을 하기전의 값이라 Magnitude 보다 빠르다.
        if (MoveVector.sqrMagnitude == 0)
            return;

        MoveVector = AdjustMoveVector(MoveVector);
        
        transform.position += MoveVector;
    }

    public void ProcessInput(Vector3 moveDirection)
    {
        MoveVector = moveDirection * (Speed * Time.deltaTime);
    }

    private Vector3 AdjustMoveVector(Vector3 moveVector)
    {
        var result = Vector3.zero;
        result = boxCollider.transform.position + boxCollider.center + moveVector;
        /*
         * boxCollider.transform.position = 현재 박스 콜라이더의 포지션
         * boxCollider.center = 내갸 center로 정해 놓은 포지션
         * moveVector 방향 벡터 * 시간
         * MainBGQuadTransform.localScale.x == 18 // ainBGQuadTransform.localScale.y = 10
         */
        
        if(result.x - boxCollider.size.x * 0.5f < -MainBGQuadTransform.localScale.x * 0.5f)
            moveVector.x = 0;
        
        if(result.x + boxCollider.size.x * 0.5f > MainBGQuadTransform.localScale.x * 0.5f) 
            moveVector.x = 0;
        
        if(result.y - boxCollider.size.y * 0.5f < -MainBGQuadTransform.localScale.y * 0.5f)
            moveVector.y = 0;
        
        if(result.y + boxCollider.size.y * 0.5f > MainBGQuadTransform.localScale.y * 0.5f)
            moveVector.y = 0;
        
        return moveVector;
    }
    
    private void OnTriggerEnter(Collider other)
    {   
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy)
            enemy.OnCrash(this, CrashDamage);
    }

    public override void OnCrash(Actor attacker, int damage)
    {
        base.OnCrash(attacker, damage);
    }

    public void Fire()
    {
        GameObject go = Instantiate(Bullet);
        Bullet bullet = go.GetComponent<Bullet>();
        bullet.Fire(this, FireTransform.position, FireTransform.right, BulletSpeed, Damage);
    }

    protected override void OnDead(Actor killer)
    {
        base.OnDead(killer);
        gameObject.SetActive(false);
    }
}
