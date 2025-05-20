using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Entity
{
    [SerializeField] private Animator anim;

    private StateMachine stateMachine;

    private DamageReceiver receiver;

    Dictionary<string, bool> Predicates = new Dictionary<string, bool>();
    private string currentAction;

    private void Start()
    {
        receiver = GetComponentInChildren<DamageReceiver>();

        stateMachine = new StateMachine();

        Predicates.Add("Head Hit", false);
        Predicates.Add("Stomach Hit", false);
        Predicates.Add("Kidney Hit", false);
        Predicates.Add("Knock Out", false);

        EnemyIdleState idleState = new EnemyIdleState(this, anim);
        EnemyHeadHitState headHitState = new EnemyHeadHitState(this, anim);
        EnemyKidneyHitState kidneyHitState = new EnemyKidneyHitState(this, anim);
        EnemyStomachHitState stomachHitState = new EnemyStomachHitState(this, anim);
        EnemyKnockOutState knockOutState = new EnemyKnockOutState(this, anim);

        Any(stomachHitState, new FuncPredicate(() => Predicates["Stomach Hit"]));
        Any(headHitState, new FuncPredicate(() => Predicates["Head Hit"]));
        Any(kidneyHitState, new FuncPredicate(() => Predicates["Kidney Hit"]));
        Any(knockOutState, new FuncPredicate(() => Predicates["Knock Out"]));


        At(stomachHitState, idleState, new FuncPredicate(() => !Predicates["Stomach Hit"]));
        At(headHitState, idleState, new FuncPredicate(() => !Predicates["Head Hit"]));
        At(kidneyHitState, idleState, new FuncPredicate(() => !Predicates["Kidney Hit"]));


        stateMachine.SetState(idleState);
    }

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    private void Update()
    {
        stateMachine.Update();

        HeadHitCheck();
        StomachHitCheck();
        KidneyHitCheck();
        KnockOutCheck();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void HeadHitCheck() => Predicates["Head Hit"] = receiver.JustGotDamage && !receiver.IsDead() && receiver.AttackAnimation == "Head Hit";
    private void StomachHitCheck() => Predicates["Stomach Hit"] = receiver.JustGotDamage && !receiver.IsDead() && receiver.AttackAnimation == "Stomach Hit";
    private void KidneyHitCheck() => Predicates["Kidney Hit"] = receiver.JustGotDamage && !receiver.IsDead() && receiver.AttackAnimation == "Kidney Hit";

    private void KnockOutCheck() => Predicates["Knock Out"] = receiver.IsDead();


    public void ResetState(string state)
    {
        if (string.IsNullOrEmpty(state))
        {
            Debug.LogWarning("Please Provide the state name in the animationClip");
            return;
        }

        Debug.Log("reset");

        Predicates[state] = false;
        currentAction = "";
        receiver.JustGotDamage = false;
    }
}
