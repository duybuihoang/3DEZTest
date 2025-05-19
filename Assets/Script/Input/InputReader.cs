using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{

    [SerializeField] private InputAction pos, press, doubleTap;

    #region threshold

    [SerializeField] private float swipeThreshold = 100f;
    [SerializeField] private float tapThreshold = 10f;
    [SerializeField] private float tapTimeThreshold = .3f;
    [SerializeField] private float doubleTapThreshold = 50f;
    [SerializeField] private float doubleTapTimeThreshold = .5f;

    #endregion

    private Vector2 initialPos;
    private Vector2 currentPos => pos.ReadValue<Vector2>();
    private float initialTapTime;

    private bool waitingForDoubleTap = false;
    private float lastTapTime = 0f;
    private Vector2 lastTapPosition = Vector2.zero;

    #region events

    public delegate void Swipe(Vector2 direction);
    public event Swipe SwipeTriggered;

    public delegate void Tap(Vector2 position);
    public event Tap TapTriggered;

    public delegate void DoubleTap(Vector2 postition);
    public event DoubleTap DoubleTapTriggered;

    #endregion

    private void Awake()
    {
        pos.Enable();
        press.Enable();

        press.performed += _ => 
        { 
            initialPos = currentPos;
            initialTapTime = Time.time;
        };

        press.canceled += _ => {
            float tapDuration = Time.time - initialTapTime;
            Vector2 delta = currentPos - initialPos;

            if(tapDuration < tapTimeThreshold && delta.magnitude < tapThreshold)
            {
                DetectTap(currentPos);
            }
            else
            {
                DetectSwipe();
                waitingForDoubleTap = false;
            }
        
        };
    }

    private void DetectSwipe()
    {
        Vector2 delta = currentPos - initialPos;
        Vector2 direction = Vector2.zero;

        if(Mathf.Abs(delta.x) > swipeThreshold)
        {
            direction.x = delta.x;
        }
        if(Mathf.Abs(delta.y) > swipeThreshold)
        {
            direction.y = delta.y;
        }

        if(direction != Vector2.zero && SwipeTriggered != null)
        {
            SwipeTriggered(direction);
        }
    }

    private void DetectTap(Vector2 position)
    {
        float currentTime = Time.time;

        if (waitingForDoubleTap &&
            (currentTime - lastTapTime) < doubleTapTimeThreshold &&
            Vector2.Distance(position, lastTapPosition) < doubleTapThreshold)
        {
            if (DoubleTapTriggered != null)
            {
                DoubleTapTriggered(position);
            }

            waitingForDoubleTap = false;
        }
        else
        {
            lastTapTime = currentTime;
            lastTapPosition = position;

            StartCoroutine(WaitForSecondTap(position));
        }
    }

    private IEnumerator WaitForSecondTap(Vector2 position)
    {
        waitingForDoubleTap = true;

        yield return new WaitForSeconds(tapTimeThreshold);

        if (waitingForDoubleTap)
        {
            if(TapTriggered != null)
            {
                TapTriggered(position);
            }
            waitingForDoubleTap = false;
        }

    }
}
