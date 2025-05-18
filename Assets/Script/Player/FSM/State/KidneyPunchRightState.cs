using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidneyPunchRightState : BaseState
{
    public KidneyPunchRightState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(KidneyPunchRight, crossFadeDuration);
    }
}
