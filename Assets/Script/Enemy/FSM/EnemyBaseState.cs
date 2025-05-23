using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected readonly EnemyController enemy;
    protected readonly Animator anim;

    protected const float crossFadeDuration = 0.0f;

    protected static readonly int Idle = Animator.StringToHash("Idle");
    protected static readonly int BigJump = Animator.StringToHash("Big Jump");
    protected static readonly int HeadPunch = Animator.StringToHash("Head Punch");
    protected static readonly int StomachPunch = Animator.StringToHash("Stomach Punch");
    protected static readonly int KidneyPunchLeft = Animator.StringToHash("Kidney Punch Left");
    protected static readonly int KidneyPunchRight = Animator.StringToHash("Kidney Punch Right");

    protected static readonly int KidneyHit = Animator.StringToHash("Kidney Hit");
    protected static readonly int HeadHit = Animator.StringToHash("Head Hit");
    protected static readonly int StomachHit = Animator.StringToHash("Stomach Hit");
    protected static readonly int Knockout = Animator.StringToHash("Knocked Out");

    protected bool isHit = false;


    public EnemyBaseState(EnemyController enemy, Animator anim)
    {
        this.enemy = enemy;
        this.anim = anim;
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void Update()
    {
    }
}
