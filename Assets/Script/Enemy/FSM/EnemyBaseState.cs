using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected readonly EnemyController enemy;
    protected readonly Animator anim;

    protected const float crossFadeDuration = 0.1f;

    protected static readonly int Idle = Animator.StringToHash("Idle");

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
