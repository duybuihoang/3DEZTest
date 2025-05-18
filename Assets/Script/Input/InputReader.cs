using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{

    [SerializeField] private InputAction pos, press;

    [SerializeField] private float swipeThreshold = 100f;
    [SerializeField] private float tapThreshold = 10f;
    [SerializeField] private float tapTimeThreshold = .3f;

    private Vector2 initialPos;
    private Vector2 currentPos => pos.ReadValue<Vector2>();
    private float initialTapTime;

    public delegate void Swipe(Vector2 direction);
    public event Swipe SwipeTriggered;

    public delegate void Tap(Vector2 position);
    public event Tap TapTriggered;

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
                if(TapTriggered != null)
                {
                    TapTriggered(currentPos);
                }
            }
            else
            {
                DetectSwipe();
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
}
