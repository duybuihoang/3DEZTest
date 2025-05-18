using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InputReader))]

public class PlayerController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;

    //[Header("Moving Data")]
    //[SerializeField] private float moveSpeed = 5f;
    //[SerializeField] float rotationSpeed = 15f;
    //[SerializeField] float smoothTime = 0.2f;

    private InputReader inputReader;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        inputReader.SwipeTriggered += Swipe;

        anim.SetBool("Idle", true);
    }

    private void Swipe(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            anim.SetBool("Idle", false);

            anim.SetBool("StomachPunch", true);
        }
    }

}
