using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigJumpState : EnemyBaseState
{
    public EnemyBigJumpState(EnemyController enemy, Animator anim) : base(enemy, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        anim.CrossFade(BigJump, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        enemy.Jump();
    }
}
