using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKidneyHitState : EnemyBaseState
{
    public EnemyKidneyHitState(EnemyController enemy, Animator anim) : base(enemy, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        if (!isHit)
        {
            isHit = true;

            anim.CrossFade(KidneyHit, crossFadeDuration);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        isHit = false;
    }
}
