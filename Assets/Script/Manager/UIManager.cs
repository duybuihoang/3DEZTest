using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    List<Button> PlayerButton = new List<Button>();
    List<Button> EnemyButton = new List<Button>();

    [SerializeField] private GameObject PlayerButtonPrefab;
    [SerializeField] private GameObject EnemyButtonPrefab;

    [SerializeField] private Transform playerUISpace;
    [SerializeField] private Transform enemyUISpace;

    public static UIManager Instance { get => instance; }
    private static UIManager instance;

    private int currentButton = 0;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void AddPlayerUIInfo()
    {
        var button = Instantiate(PlayerButtonPrefab, playerUISpace).GetComponent<Button>();
        button.interactable = true;
        button.GetComponentInChildren<TextMeshProUGUI>().text = (PlayerButton.Count + 1).ToString();

        button.GetComponentInChildren<Slider>().value = 1;
        button.GetComponentInChildren<Slider>().interactable = false;


        button.onClick.AddListener(() => OnButtonClick(button));
        PlayerButton.Add(button);

    }

    public void ResetUI()
    {
        PlayerButton.Clear();
    }

    public void AddEnemyUIInfo(float maxHealth)
    {
        EnemyButton.Add(Instantiate(EnemyButtonPrefab, enemyUISpace).GetComponent<Button>());
    }

    private void OnButtonClick(Button button)
    {
        int index = PlayerButton.IndexOf(button);
        Debug.Log(index);
        PlayerButton[currentButton].interactable = true;
        PlayerButton[index].interactable = false;
        currentButton = index;

        

        GameManager.Instance.swapCharacter(index);
    }

    public void DisableButton(int idx)
    {
        PlayerButton[idx].interactable = false;
    }

    public void UpdateSlider(int idx,float value)
    {
        PlayerButton[idx].GetComponentInChildren<Slider>().value = value;
    }

    public void DestroyButton()
    {
        PlayerButton.RemoveAt(currentButton);
        currentButton = 0;
        PlayerButton[currentButton].interactable = true;
    }

}
