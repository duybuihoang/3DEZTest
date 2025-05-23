using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHeadHitState : EnemyBaseState
{
    public EnemyHeadHitState(EnemyController enemy, Animator anim) : base(enemy, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();


        if (!isHit)
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
