using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    private bool canAttack = false;
    public bool CanAttack { get => canAttack; }
    [SerializeField] private DamageReceiver receiver;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");

        if (other.tag == "receiver")
        {
            receiver = other.GetComponentInChildren<DamageReceiver>();
            Debug.Log(receiver);
            if (receiver)
            {
                Debug.Log(transform.parent.name + " canAttack to : " + receiver);
                canAttack = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");

        canAttack = false;
        receiver = null;

    }

    public void Send(float amount, string anim)
    {
        Debug.Log(receiver);
        receiver?.Deduct(amount, anim);
    }
}
