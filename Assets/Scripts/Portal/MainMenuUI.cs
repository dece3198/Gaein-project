using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player.SetActive(false);
    }

    public void StartButton()
    {
        mainMenu.SetActive(false);
        player.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
