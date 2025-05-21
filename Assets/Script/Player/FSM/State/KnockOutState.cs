using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOutState : BaseState
{
    public KnockOutState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(Knockout, crossFadeDuration);
    }
}
