using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly Animator anim;
    protected readonly PlayerController player;

    protected float crossFadeDuration = 0.0f;

    #region Animation Hash
    protected static readonly int Idle = Animator.StringToHash("Idle");
    protected static readonly int HeadPunch = Animator.StringToHash("Head Punch");
    protected static readonly int StomachPunch = Animator.StringToHash("Stomach Punch");
    protected static readonly int KidneyPunchLeft = Animator.StringToHash("Kidney Punch Left");
    protected static readonly int KidneyPunchRight = Animator.StringToHash("Kidney Punch Right");

    #endregion

    protected BaseState(PlayerController player, Animator anim)
    {
        this.player = player;
        this.anim = anim;
    }

    public virtual void OnEnter()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void Update()
    {
    }
}
