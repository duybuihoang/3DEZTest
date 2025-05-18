using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPunchState : BaseState
{
    public HeadPunchState(PlayerController player, Animator anim) : base(player, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(HeadPunch, crossFadeDuration);
    }
}
