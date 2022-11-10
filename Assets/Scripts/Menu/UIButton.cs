using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Button clickImage;

    private void Awake()
    {
        clickImage.gameObject.SetActive(false);
    }


    public void MenuButtonClick()
    {
        if(UIManager.instance.curButton != null)
        {
            UIManager.instance.curButton.gameObject.SetActive(false);
        }
        UIManager.instance.curButton = clickImage;
        clickImage.gameObject.SetActive(true);
    }
}
