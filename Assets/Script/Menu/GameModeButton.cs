using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class GameModeButton : MonoBehaviour
{
    [SerializeField] private List<Button> buttons = new List<Button>();
    private GameMode currentGameMode = GameMode.Single;

    private void Awake()
    {
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnGameModeClicked(button));
        }
    }

    public void OnGameModeClicked(Button button)
    {
        foreach (var item in buttons)
        {
            if (item != button)
                item.interactable = true;
        }

        button.interactable = false;

        currentGameMode = (GameMode)buttons.IndexOf(button);
    }

    public GameMode GetGameMode() => currentGameMode;
}
