using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly Animator anim;
    protected readonly PlayerController player;

    protected float crossFadeDuration = 0.1f;

    protected BaseState(PlayerController player, Animator anim)
    {
        this.player = player;
        this.anim = anim;
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
