using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Button clickImage;

    private void Awake()
    {
        if(clickImage != null)
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

    public void FoodButtonClick(Food food)
    {
        if (Gold.Instance.Minus(food))
        {
            this.gameObject.SetActive(false);
            switch (food.foodType)
            {
                case Food.FOOD_TYPE.ApplePie: Guest.instance.foodList.Add(food); break;
                case Food.FOOD_TYPE.SpecialSet: Guest.instance.foodList.Add(food); break;
            }
        }
    }

}
