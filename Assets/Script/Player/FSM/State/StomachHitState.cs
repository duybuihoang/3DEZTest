using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomachHitState : BaseState
{
    public StomachHitState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.ResetDict();

        if (!isHit && !isJumping)
        {
            isHit = true;
            anim.CrossFade(StomachHit, crossFadeDuration);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        isHit = false;
    }
}
