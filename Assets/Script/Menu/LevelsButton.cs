using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelsButton : MonoBehaviour
{
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private GameModeButton gameMode;

    private void Awake()
    {
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnLevelClicked(button));
        }
    }

    private void OnLevelClicked(Button button)
    {
        int level = buttons.IndexOf(button) + 1;
        //change scene


        LevelLoader.level = level;
        LevelLoader.gameMode = gameMode.GetGameMode();

        SceneManager.LoadScene("Demo");
    }
}
