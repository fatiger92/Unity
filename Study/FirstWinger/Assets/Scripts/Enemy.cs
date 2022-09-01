using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Actor
{
    public enum State
    {
        None = -1, // 사용전
        Ready,     // 준비완료
        Appear,    // 등장
        Battle,    // 전투중
        Dead,      // 사망
        Disappear  // 퇴장
    }

    [SerializeField] private State CurrentState = State.None;

    private const float MaxSpeed = 10f;
    private const float MaxSpeedTime = 0.5f;

    [SerializeField] private Vector3 TargetPosition;
    [SerializeField] private float CurrentSpeed;

    private Vector3 CurrentVelocity;

    private float MoveStartTime = 0.0f;
    
    [SerializeField] private Transform FireTransform;
    [SerializeField] private float BulletSpeed = 1f;
    
    private float LastBattleUpdateTime = 0.0f;

    [SerializeField] private int FireRemainCount = 1;

    [SerializeField] private int GamePoint = 10;
    public string FilePath { get; set; }
    protected override void UpdateActor()
    {
        switch (CurrentState)
        {
            case State.None:
                break;
            case State.Ready:
                break;
            case State.Appear:
            case State.Disappear:
                UpdateSpeed();
                UpdateMove();
                break;
            case State.Battle:
                UpdateBattle();
                break;
            case State.Dead:
                break;
        }
    }

    private void UpdateSpeed()
    {
        // (Time.time - MoveStartTime ) 이거 한 이유가 뭐임? 조금 더 부드럽게 움직이게 하기 위해서?
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, (Time.time - MoveStartTime ) / MaxSpeedTime);
        
        //CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, MaxSpeedTime);
    }

    private void UpdateMove()
    {
        var distance = Vector3.Distance(TargetPosition, transform.position);

        if (distance == 0)
        {
            Arrived();
            return;
        }

        // Velocity(속도) = Speed(속력) * 방향
        CurrentVelocity = (TargetPosition - transform.position).normalized * CurrentSpeed;
        
        // 속도 = 거리 / 시간 :: 시간 = 거리 / 속도
        transform.position =  Vector3.SmoothDamp(transform.position, TargetPosition, ref CurrentVelocity, distance / CurrentSpeed, MaxSpeed);
    }

    private void Arrived()
    {
        CurrentSpeed = 0f;
        
        if (CurrentState == State.Appear)
        {
            CurrentState = State.Battle;
            LastBattleUpdateTime = Time.time;
        }
        else // if (CurrentState == State.Disappear)
        {
            CurrentState = State.None;
            SystemManager.Instance.EnemyManager.RemoveEnemy(this);
        }
    }

    public void Appear(Vector3 targetPos)
    {
        TargetPosition = targetPos;
        CurrentSpeed = MaxSpeed;

        CurrentState = State.Appear;
        MoveStartTime = Time.time;
    }

    private void Disappear(Vector3 targetPos)
    {
        TargetPosition = targetPos;
        //CurrentSpeed = 0; // 하는 이유가 뭔지?
        
        CurrentState = State.Disappear;
        MoveStartTime = Time.time;
    }

    private void UpdateBattle()
    {
        if (Time.time - LastBattleUpdateTime > 1.0f)
        {
            if (FireRemainCount > 0)
            {
                Fire();
                FireRemainCount--;
            }
            else
            {
                Disappear(new Vector3(-15f, transform.position.y, transform.position.z));
            }
            LastBattleUpdateTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            if (!player.IsDead)
            {
                BoxCollider box = other as BoxCollider;
                var crashPos = player.transform.position + box.center;
                crashPos.x += box.size.x * 0.5f;
                
                player.OnCrash(this, CrashDamage, crashPos);
            }
        }
    }
    
    public override void OnCrash(Actor attacker, int damage, Vector3 crashPos)
    {
        base.OnCrash(attacker ,damage, crashPos);
    }
    
    public void Fire()
    {
        Bullet bullet = SystemManager.Instance.BulletManager.Generate(BulletManager.EnemyBulletIndex);
        bullet.Fire(this, FireTransform.position, -FireTransform.right, BulletSpeed, Damage);
    }
    
    protected override void OnDead(Actor killer)
    {
        base.OnDead(killer);
        SystemManager.Instance.GamePointAccumulator.Accumulate(GamePoint);
        SystemManager.Instance.EnemyManager.RemoveEnemy(this);
        
        CurrentState = State.Dead;
    }
    
    protected override void DecreaseHP(Actor attacker, int value, Vector3 damagePos)
    {
        base.DecreaseHP(attacker, value, damagePos);

        Vector3 damagePoint = damagePos + Random.insideUnitSphere * 0.5f;
        SystemManager.Instance.DamageManager.Generate(DamageManager.EnemyDamageIndex, damagePoint, value, Color.magenta);
    }
}
