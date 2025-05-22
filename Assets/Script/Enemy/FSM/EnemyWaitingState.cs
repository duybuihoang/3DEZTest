using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitingState : EnemyBaseState
{
    public EnemyWaitingState(EnemyController enemy, Animator anim) : base(enemy, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(Idle, crossFadeDuration);
    }
}
