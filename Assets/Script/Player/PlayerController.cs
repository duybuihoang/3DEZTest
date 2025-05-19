using System.Collections;
using System.Collections.Generic;
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
    

    private InputReader inputReader;
    private StateMachine stateMachine;

    private bool rotateBack = false;

    private Vector3 forwardDirection = new Vector3(-1, 0, 1);
    private Vector3 backwardDirection = new Vector3(1, 0, -1);


    private void Awake()
    {
        Predicates.Add("HeadPunch", false);
        Predicates.Add("KidneyPunchLeft", false);
        Predicates.Add("KidneyPunchRight", false);
        Predicates.Add("Stomach Punch", false);
        Predicates.Add("Big Jump", false);


        #region Components

        inputReader = GetComponent<InputReader>();
        //rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
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
        #endregion

        stateMachine.SetState(idleState);
    }

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition); 
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    private void Swipe(Vector2 direction)
    {
        if(Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            Predicates["Big Jump"] = true;
            if(direction.y < 0)
            {
                rotateBack = true;
            }

        }
        else if (Mathf.Abs(direction.y) <= Mathf.Abs(direction.x))
        {
            if(direction.x > 0)
            {
                Predicates["KidneyPunchLeft"] = true;
            }
            else
            {
                Predicates["KidneyPunchRight"] = true;
            }
        }
    }

    private void Tap(Vector2 position)
    {
        Predicates["HeadPunch"] = true;
    }

    private void DoubleTap(Vector2 position)
    {
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

        controller.Move(jumpDirection * moveSpeed * Time.deltaTime);

        transform.rotation = targetRotation;

    }


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
    }
}
