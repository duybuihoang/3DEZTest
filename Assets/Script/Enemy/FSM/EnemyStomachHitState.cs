using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomachHitState : EnemyBaseState
{
    public EnemyStomachHitState(EnemyController enemy, Animator anim) : base(enemy, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("stomach hit");
        if (!isHit)
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
