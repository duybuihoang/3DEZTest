using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigJumpState : BaseState
{
    public BigJumpState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(BigJump, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.Jump();
    }
}
