using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomachPunchState : EnemyBaseState
{
    public EnemyStomachPunchState(EnemyController enemy, Animator anim) : base(enemy, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(StomachPunch, crossFadeDuration);
    }
}
