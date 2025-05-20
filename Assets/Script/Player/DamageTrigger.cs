using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " entered");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " exited");

    }
}
