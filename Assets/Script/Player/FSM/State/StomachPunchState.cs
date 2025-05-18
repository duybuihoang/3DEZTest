using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomachPunchState : BaseState
{
    public StomachPunchState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(StomachPunch, crossFadeDuration);

    }
}
