using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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

    public List<Transform> playerTeamPlace = new List<Transform>();
    public List<Transform> enemyTeamPlace = new List<Transform>();

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

        instance = this;
        //DontDestroyOnLoad(gameObject);

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

        for (int i = 1; i < playerTeam.Count; i++)
        {
            playerTeam[i] = Instantiate(playerPrefab, playerTeamPlace[i - 1].transform);   
        }

        for (int i = 1; i < enemyTeam.Count; i++)
        {
            enemyTeam[i] = Instantiate(enemyPrefab, enemyTeamPlace[i - 1].transform);
        }


        playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().EnterArena();
        enemyTeam[enemyTeamCurrentIndex].GetComponent<EnemyController>().EnterArena();
    }

    public GameObject GetCurrentPlayer() => playerTeam[playerTeamCurrentIndex];
    public GameObject GetCurrentEnemy() => enemyTeam[enemyTeamCurrentIndex];

    [ContextMenu("swap")]
    public void swapCharacter()
    {
        playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().ExitArena();

        int nextPlayerIndex = (playerTeamCurrentIndex + 1) % playerTeam.Count;

        var tempPosition = playerTeam[playerTeamCurrentIndex].transform.position;
        playerTeam[playerTeamCurrentIndex].transform.position = playerTeam[nextPlayerIndex].transform.position;
        playerTeam[nextPlayerIndex].transform.position = tempPosition;

        playerTeamCurrentIndex = nextPlayerIndex;
        playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().EnterArena();
    }

    public void OnCharacterKnockOut(GameObject GO)
    {
        var player = GO.GetComponent<PlayerController>();
        if (player)
        {
            int knockOutIndex = playerTeamCurrentIndex;
            playerTeamCurrentIndex = 0;

            playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().ExitArena();

            int nextPlayerIndex = (playerTeamCurrentIndex + 1) % playerTeam.Count;

            var tempPosition = playerTeam[playerTeamCurrentIndex].transform.position;
            playerTeam[playerTeamCurrentIndex].transform.position = playerTeam[nextPlayerIndex].transform.position;
            playerTeam[nextPlayerIndex].transform.position = tempPosition;

            playerTeamCurrentIndex = nextPlayerIndex;
            playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().EnterArena();

            playerTeam.Remove(playerTeam[knockOutIndex]);

            return;
        }

        var enemy = GO.GetComponent<EnemyController>();
        if (enemy)
        {
            int knockOutIndex = enemyTeamCurrentIndex;
            enemyTeamCurrentIndex = 0;

            enemyTeam[enemyTeamCurrentIndex].GetComponent<EnemyController>().ExitArena();

            int nextEnemyIndex = (enemyTeamCurrentIndex + 1) % enemyTeam.Count;

            var tempPosition = enemyTeam[enemyTeamCurrentIndex].transform.position;
            enemyTeam[enemyTeamCurrentIndex].transform.position = enemyTeam[nextEnemyIndex].transform.position;
            enemyTeam[nextEnemyIndex].transform.position = tempPosition;

            enemyTeamCurrentIndex = nextEnemyIndex;
            enemyTeam[enemyTeamCurrentIndex].GetComponent<EnemyController>().EnterArena();

            enemyTeam.Remove(enemyTeam[knockOutIndex]);

            return;
        }

        return;
    }


}
