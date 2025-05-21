using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameMode
{
    Single, 
    SingleVsMultiple, 
    MultipleVsMultiple
        
};

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameMode mode;
    [SerializeField] private GameObject currentPlayer;
    public GameObject currentPlayer { get => currentPlayer; }
    [SerializeField] private GameObject currentEnemy;
    public GameObject CurrentEnemy { get => currentPlayer; }

    public Queue<GameObject> 


}
