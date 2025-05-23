using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public enum GameMode
{
    Single, 
    SingleVsMultiple, 
    MultipleVsMultiple
        
};

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameMode mode;
    private int Level = 1;

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

    private float maxDelayTime = 2f;
    [SerializeField] private float delayConstant = 0.15f;

    private int maxLevel = 10;


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
        mode = LevelLoader.gameMode;
        Level = LevelLoader.level;
        SetUpGame();
    }

    public void SetUpGame()
    {
        playerTeam.Clear();
        enemyTeam.Clear();
        UIManager.Instance.ResetUI();

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

        for (int i = 0; i < playerTeam.Count; i++)
        {
            UIManager.Instance.AddPlayerUIInfo();
        }


        playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().EnterArena();
        playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().SetTarget();

        enemyTeam[enemyTeamCurrentIndex].GetComponent<EnemyController>().EnterArena();
        enemyTeam[enemyTeamCurrentIndex].GetComponent<EnemyController>().SetTarget();

        UIManager.Instance.DisableButton(0);
        SetEnemyDelayTime();
    }

    public GameObject GetCurrentPlayer() => playerTeam[playerTeamCurrentIndex];
    public GameObject GetCurrentEnemy() => enemyTeam[enemyTeamCurrentIndex];

    [ContextMenu("swap")]
    public void swapCharacter(int idx)
    {

        playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().ExitArena();

        var temp = playerTeam[playerTeamCurrentIndex].transform.position;
        playerTeam[playerTeamCurrentIndex].transform.position = playerTeam[idx].transform.position;
        playerTeam[idx].transform.position = temp;

        playerTeamCurrentIndex = idx;
        playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().EnterArena();

        playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().SetTarget();
        enemyTeam[enemyTeamCurrentIndex].GetComponent<EnemyController>().SetTarget();





    }

    public void OnCharacterKnockOut(GameObject GO)
    {
        var player = GO.GetComponent<PlayerController>();
        if (player)
        {
            if (playerTeam.Count <= 1)
            {
                SceneManager.LoadScene("Menu");

            }
            else
            {
                UIManager.Instance.DisableButton(playerTeamCurrentIndex);
                UIManager.Instance.DestroyButton();

                playerTeam.Remove(playerTeam[playerTeamCurrentIndex]);
                playerTeamCurrentIndex = 0;
                playerTeam[playerTeamCurrentIndex].transform.position = playerPlace.position;
                playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().EnterArena();

                playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().SetTarget();
                enemyTeam[enemyTeamCurrentIndex].GetComponent<EnemyController>().SetTarget();
            }
            return;
        }

        var enemy = GO.GetComponent<EnemyController>();
        if (enemy)
        {
            if (enemyTeam.Count <= 1)
            {
                Debug.Log("Win");
                SceneManager.LoadScene("Menu");
            }
            else
            {
                enemyTeam.Remove(enemyTeam[enemyTeamCurrentIndex]);
                enemyTeamCurrentIndex = 0;
                enemyTeam[enemyTeamCurrentIndex].transform.position = enemyPlace.position;
                enemyTeam[enemyTeamCurrentIndex].GetComponent<EnemyController>().EnterArena();
                playerTeam[playerTeamCurrentIndex].GetComponent<PlayerController>().SetTarget();
                enemyTeam[enemyTeamCurrentIndex].GetComponent<EnemyController>().SetTarget();
            }

            return;
        }

        return;
    }
    public void UpdatePlayerSlider(float amount)
    {
        UIManager.Instance.UpdateSlider(playerTeamCurrentIndex, amount);
    }

    public void SetEnemyDelayTime()
    {
        foreach (var enemy in enemyTeam)
        {
            enemy.GetComponent<EnemyController>().SetDelayTime(
                calculateDelayByLevel(LevelLoader.level), calculateDelayByLevel(LevelLoader.level + 1));
        }
    }


    private float calculateDelayByLevel(int level)
    {
        if (maxLevel - level == 0) return 0;
        return maxDelayTime - delayConstant * level;
    }
}
