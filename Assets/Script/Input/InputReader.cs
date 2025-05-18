using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{

    [SerializeField] private InputAction pos, press;

    [SerializeField] private float swipeThreshold = 100f;

    private Vector2 initialPos;
    private Vector2 currentPos => pos.ReadValue<Vector2>();

    public delegate void Swipe(Vector2 direction);
    public event Swipe SwipeTriggered;

    private void Awake()
    {
        pos.Enable();
        press.Enable();

        press.performed += _ => { initialPos = currentPos; };
        press.canceled += _ => { DetectSwipe(); };
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
