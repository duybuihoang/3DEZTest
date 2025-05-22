using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : BaseState
{
    public WaitingState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(Idle, crossFadeDuration);
    }
}
