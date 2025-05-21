using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadHitState : BaseState
{
    public HeadHitState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        player.ResetDict();

        if (!isHit && !isJumping)
        {
            isHit = true;

            anim.CrossFade(HeadHit, crossFadeDuration);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        isHit = false;
    }
}
