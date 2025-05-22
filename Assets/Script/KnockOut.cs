using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOut : MonoBehaviour
{
   public void OnCharacterKnockOut(GameObject GO)
    {
        var player = GO.GetComponent<PlayerController>();
        if(player)
        {
            
            return;
        }

        var enemy = GO.GetComponent<EnemyController>();
        if(enemy)
        {
            return;
        }

        return;
    }
}
