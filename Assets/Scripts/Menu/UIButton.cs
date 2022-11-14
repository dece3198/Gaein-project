using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButton : Singleton<UIButton>
{
    [SerializeField] private Button clickImage;
    public bool isPie = false;

    UnityAction onClick;

    private void Awake()
    {
        if(clickImage != null)
        clickImage.gameObject.SetActive(false);
    }


    public void MenuButtonClick()
    {
        if(UIManager.Instance.curButton != null)
        {
            UIManager.Instance.curButton.gameObject.SetActive(false);
        }
        UIManager.Instance.curButton = clickImage;
        clickImage.gameObject.SetActive(true);
    }

    public void FoodButtonClick(Food food)
    {
        if (GoldManager.Instance.Minus(food))
        {
            this.gameObject.SetActive(false);
        }
    }

}
