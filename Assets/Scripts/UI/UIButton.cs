using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Button clickImage;
    public bool isPie = false;

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

    public void FoodButtonClick(ScriptableObject scriptable)
    {
        switch (scriptable)
        {
            case Food:
                Food food = scriptable as Food;
                if(GoldManager.Instance.Minus(food))
                {
                    MenuList.Instance.menuList.Add(food);
                    this.gameObject.SetActive(false);
                }
                break;
            case Ingredients:
                Ingredients ingredients = scriptable as Ingredients;
                if (GoldManager.Instance.Minus(ingredients))
                {
                    ingredients.Count += 1;
                }
                break;
        }

    }
}
