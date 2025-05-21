using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    public List<GameObject> playerTeam = new List<GameObject>();
    public List<GameObject> enemyTeam = new List<GameObject>();
    private int playerTeamCurrentIndex = 0;
    private int enemyTeamCurrentIndex = 0;

    [SerializeField] private Transform playerPlace;
    [SerializeField] private Transform enemyPlace;
    public static GameManager Instance { get => instance; }
    private static GameManager instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance and mark it to not be destroyed between scenes
        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {

        switch (mode)
        {
            case GameMode.Single:
                playerTeam.Add(playerPrefab);
                enemyTeam.Add(enemyPrefab);
                break;
            case GameMode.SingleVsMultiple:
                playerTeam.Add(playerPrefab);
                enemyTeam.Add(enemyPrefab);
                enemyTeam.Add(enemyPrefab);
                enemyTeam.Add(enemyPrefab);
                break;
            case GameMode.MultipleVsMultiple:
                playerTeam.Add(playerPrefab);
                playerTeam.Add(playerPrefab);
                playerTeam.Add(playerPrefab);
                enemyTeam.Add(enemyPrefab);
                enemyTeam.Add(enemyPrefab);
                enemyTeam.Add(enemyPrefab);
                break;
            default:
                break;
        }

        playerTeam[playerTeamCurrentIndex] = Instantiate(playerPrefab, playerPlace);
        enemyTeam[enemyTeamCurrentIndex] = Instantiate(enemyPrefab, enemyPlace);
    }

    public GameObject GetCurrentPlayer() => playerTeam[playerTeamCurrentIndex];
    public GameObject GetCurrentEnemy() => enemyTeam[enemyTeamCurrentIndex];
}
