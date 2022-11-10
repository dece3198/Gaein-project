using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static bool inventoryActivated = false;
    [SerializeField] private GameObject mainUI;
    public Button curButton;

    private void Awake()
    {
        instance = this;
        mainUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
            {
                mainUI.SetActive(true);
            }
            else
            {
                mainUI.SetActive(false);
            }

        }

        if (mainUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
