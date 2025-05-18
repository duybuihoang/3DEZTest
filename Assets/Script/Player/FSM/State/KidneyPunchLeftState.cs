using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidneyPunchLeftState : BaseState
{
    public KidneyPunchLeftState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(KidneyPunchLeft, crossFadeDuration);
    }
}
