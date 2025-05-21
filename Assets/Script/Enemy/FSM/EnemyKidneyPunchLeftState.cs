using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKidneyPunchLeftState : EnemyBaseState
{
    public EnemyKidneyPunchLeftState(EnemyController enemy, Animator anim) : base(enemy, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(KidneyPunchLeft, crossFadeDuration);
    }
}
