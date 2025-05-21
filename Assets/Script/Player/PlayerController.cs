using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InputReader))]

public class PlayerController : MonoBehaviour
{
    [Header("Reference")]
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator anim;
    

    [Header("Moving Data")]
    [SerializeField] private float moveSpeed = 2f;

    Dictionary<string, bool> Predicates = new Dictionary<string, bool>();
    private string currentAction;
    

    private InputReader inputReader;
    private StateMachine stateMachine;

    private bool rotateBack = false;

    private Vector3 forwardDirection = new Vector3(-1, 0, 1);
    private Vector3 backwardDirection = new Vector3(1, 0, -1);

    private DamageSender sender;
    private DamageReceiver receiver;

    [SerializeField]private GameObject target;


    private void Awake()
    {
        Predicates.Add("HeadPunch", false);
        Predicates.Add("KidneyPunchLeft", false);
        Predicates.Add("KidneyPunchRight", false);
        Predicates.Add("Stomach Punch", false);
        Predicates.Add("Big Jump", false);

        Predicates.Add("Head Hit", false);
        Predicates.Add("Stomach Hit", false);
        Predicates.Add("Kidney Hit", false);
        Predicates.Add("Knock Out", false);


        #region Components

        inputReader = GetComponent<InputReader>();
        //rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        sender = GetComponentInChildren<DamageSender>();
        receiver = GetComponentInChildren<DamageReceiver>();

        stateMachine = new StateMachine();

        #endregion
        #region subscribe input

        inputReader.SwipeTriggered += Swipe;
        inputReader.TapTriggered += Tap;
        inputReader.DoubleTapTriggered += DoubleTap;

        #endregion
        #region state

        IdleState idleState = new IdleState(this, anim);
        HeadPunchState headPunchState = new HeadPunchState(this, anim);
        KidneyPunchLeftState kidneyPunchLeftState = new KidneyPunchLeftState(this, anim);
        KidneyPunchRightState kidneyPunchRightState = new KidneyPunchRightState(this, anim);
        StomachPunchState stomachPunchState = new StomachPunchState(this, anim);
        BigJumpState bigJumpState = new BigJumpState(this, anim);

        HeadHitState headHitState = new HeadHitState(this, anim);
        KidneyHitState kidneyHitState = new KidneyHitState(this, anim);
        StomachHitState stomachHitState = new StomachHitState(this, anim);
        KnockOutState knockOutState = new KnockOutState(this, anim);


        #endregion
        #region transition

        At(idleState, headPunchState, new FuncPredicate(() => Predicates["HeadPunch"]));
        At(headPunchState, idleState, new FuncPredicate(() => !Predicates["HeadPunch"]));

        At(idleState, kidneyPunchLeftState, new FuncPredicate(() => Predicates["KidneyPunchLeft"]));
        At(kidneyPunchLeftState, idleState, new FuncPredicate(() => !Predicates["KidneyPunchLeft"]));

        At(idleState, kidneyPunchRightState, new FuncPredicate(() => Predicates["KidneyPunchRight"]));
        At(kidneyPunchRightState, idleState, new FuncPredicate(() => !Predicates["KidneyPunchRight"]));

        At(idleState, stomachPunchState, new FuncPredicate(() => Predicates["Stomach Punch"]));
        At(stomachPunchState, idleState, new FuncPredicate(() => !Predicates["Stomach Punch"]));

        At(idleState, bigJumpState, new FuncPredicate(() => Predicates["Big Jump"]));
        At(bigJumpState, idleState, new FuncPredicate(() => !Predicates["Big Jump"]));

        Any(stomachHitState, new FuncPredicate(() => Predicates["Stomach Hit"]));
        Any(headHitState, new FuncPredicate(() => Predicates["Head Hit"]));
        Any(kidneyHitState, new FuncPredicate(() => Predicates["Kidney Hit"]));
        Any(knockOutState, new FuncPredicate(() => Predicates["Knock Out"]));


        At(stomachHitState, idleState, new FuncPredicate(() => !Predicates["Stomach Hit"]));
        At(headHitState, idleState, new FuncPredicate(() => !Predicates["Head Hit"]));
        At(kidneyHitState, idleState, new FuncPredicate(() => !Predicates["Kidney Hit"]));


        #endregion

        stateMachine.SetState(idleState);
    }

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition); 
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
    private void Swipe(Vector2 direction)
    {
        if(Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            currentAction = "Big Jump";
            if(direction.y < 0)
            {
                rotateBack = true;
            }

        }
        else if (Mathf.Abs(direction.y) <= Mathf.Abs(direction.x))
        {
            if(direction.x > 0)
            {
                currentAction = "KidneyPunchLeft";
            }
            else
            {
                currentAction = "KidneyPunchRight";
            }
        }

        Predicates[currentAction] = true;
    }
    private void Tap(Vector2 position)
    {
        currentAction = "HeadPunch";
        Predicates["HeadPunch"] = true;
    }
    private void DoubleTap(Vector2 position)
    {
        currentAction = "Stomach Punch";
        Predicates["Stomach Punch"] = true;
    }
    public void Jump()
    {
        
        Vector3 jumpDirection;
        Quaternion targetRotation;

        if (rotateBack)
        {
            jumpDirection = backwardDirection;
            targetRotation = Quaternion.LookRotation(backwardDirection);
            
        }
        else
        {
            jumpDirection = forwardDirection;
            targetRotation = Quaternion.LookRotation(forwardDirection);
        }
        transform.rotation = targetRotation;


        if (Vector3.Distance(transform.position, target.transform.position) < 0.7f && jumpDirection == forwardDirection) return;
        
        controller.Move(jumpDirection * moveSpeed * Time.deltaTime);
        

    }
    //private void HeadHitCheck() => Predicates["Head Hit"] = receiver.JustGotDamage && !receiver.IsDead() && receiver.AttackAnimation == "Head Hit";

    private void DoHitCheck(string name)
    {
        Predicates[name] = receiver.JustGotDamage && !receiver.IsDead() && receiver.AttackAnimation == name;
        if (Predicates[name])
        {
            Predicates[currentAction] = false;
            currentAction = "";
            currentAction = name;
        }
    }

    private void HeadHitCheck()
    {
        Predicates["Head Hit"] = receiver.JustGotDamage && !receiver.IsDead() && receiver.AttackAnimation == "Head Hit";
    }

    private void StomachHitCheck() => Predicates["Stomach Hit"] = receiver.JustGotDamage && !receiver.IsDead() && receiver.AttackAnimation == "Stomach Hit";
    private void KidneyHitCheck() => Predicates["Kidney Hit"] = receiver.JustGotDamage && !receiver.IsDead() && receiver.AttackAnimation == "Kidney Hit";
    private void KnockOutCheck() => Predicates["Knock Out"] = receiver.IsDead();

    public void ResetRotation()
    {
        if (rotateBack)
        {
            rotateBack = false;
            transform.rotation = Quaternion.LookRotation(backwardDirection);
        }
    }

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
    public void ResetState(string state)
    {
        if (string.IsNullOrEmpty(state))
        {
            Debug.LogWarning("Please Provide the state name in the animationClip");
            return;
        }

        Predicates[state] = false;
        currentAction = "";
        receiver.JustGotDamage = false;
    }

    public void ResetDict()
    {
        List<string> keys = new List<string>(Predicates.Keys);
        foreach (var key in keys)
        {
            Predicates[key] = false;
        }
    }

    public void SendDamage()
    {
        string hitAnimation = "";
        int damage = 0;
        switch (currentAction)
        {
            case "HeadPunch":
                hitAnimation = "Head Hit";
                damage = 1;
                break;
            case "KidneyPunchLeft":
                hitAnimation = "Kidney Hit";
                damage = 2;
                break;
            case "KidneyPunchRight":
                hitAnimation = "Kidney Hit";
                damage = 3;
                break;
            case "Stomach Punch":
                hitAnimation = "Stomach Hit";
                damage = 2;
                break;
            default:
                break;
        }

        sender.Send(damage, hitAnimation);
    }

}
