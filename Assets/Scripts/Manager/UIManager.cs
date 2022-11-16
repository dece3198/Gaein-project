using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public static bool inventoryActivated = false;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private TextMeshProUGUI starText;
    [SerializeField] private Button Button;
    public Button curButton;
    public int starValue = 1;

    private void Awake()
    {
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

        starText.text = starValue.ToString();

        if (mainUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if(Button != null)
            {
                if(Button.gameObject.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.None;
                    return;
                }
            }
            if(ConversationController.Instance.butcherShop != null)
            {
                if (ConversationController.Instance.butcherShop.gameObject.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.None;
                    return;
                }
            }
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
