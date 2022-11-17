using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject telepoUI;
    public PlayerCamera playerCamera;

    private void Awake()
    {
        telepoUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            playerCamera = other.transform.GetChild(0).GetComponent<PlayerCamera>();
            playerCamera.enabled = false;
            telepoUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            telepoUI.SetActive(false);
            playerCamera.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void MarketButton()
    {
        if(playerCamera != null)
        {
            playerCamera.enabled = true;
        }
        telepoUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Market");
    }

    public void BackButton()
    {
        telepoUI.SetActive(false);
        playerCamera.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Game");
    }
}
