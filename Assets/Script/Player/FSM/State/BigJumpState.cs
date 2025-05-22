using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigJumpState : BaseState
{
    public BigJumpState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (!isJumping)
        {
            anim.CrossFade(BigJump, crossFadeDuration);
            isJumping = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        isJumping = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.Jump();
    }
}
