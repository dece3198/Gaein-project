using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject telepoUI;
    [SerializeField] private GameObject RequestUI;
    public PlayerCamera playerCamera;

    private void Awake()
    {
        telepoUI.SetActive(false);
        if(RequestUI != null)
        RequestUI.SetActive(false);
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
            RequestUI.SetActive(false);
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
    public void Request()
    {
        telepoUI.SetActive(false);
        RequestUI.SetActive(true);
    }
    public void Battle()
    {
        RequestUI.SetActive(false);
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Battle");
    }
    public void BackButton()
    {
        telepoUI.SetActive(false);
        playerCamera.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Game");
    }
}
