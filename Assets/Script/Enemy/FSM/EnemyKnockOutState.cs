using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyKnockOutState : EnemyBaseState
{
    public EnemyKnockOutState(EnemyController enemy, Animator anim) : base(enemy, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(Knockout, crossFadeDuration);
    }
}
