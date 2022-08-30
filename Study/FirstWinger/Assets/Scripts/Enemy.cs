using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float BulletSpeed = 1f;
    
    private float LastBattleUpdateTime = 0.0f;

    [SerializeField] private int FireRemainCount = 1;
    
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
            player.OnCrash(this);
    }
    
    public void OnCrash(Player player)
    {
        Debug.Log($"OnCrash player = {player}");
    }
    
    public void Fire()
    {
        GameObject go = Instantiate(Bullet);
        Bullet bullet = go.GetComponent<Bullet>();
        bullet.Fire(OwnerSide.Enemy, FireTransform.position, -FireTransform.right, BulletSpeed, Damage);
    }
}
