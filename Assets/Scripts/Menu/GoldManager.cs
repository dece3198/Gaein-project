using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class GoldManager : Singleton<GoldManager>
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI silverText;
    public TextMeshProUGUI bronzeText;
    public int gold;
    public int silver;
    public int bronze;


    public int Bronze
    { 
        get 
        {
            if(bronze < 1000) return bronze;
            else return bronze % 1000;
        }
        set 
        {
            bronze = value;
            if (bronze / 1000 >= 1)
            {
                Silver += bronze / 1000;
            }
        }
    }
    public int Silver
    {
        get 
        {
            if (silver >= 1000) return silver % 1000;
            else return silver;
        }
        set
        {
            silver = value;
            if (silver >= 1000)
            {
                Gold += silver / 1000;
            }
        }
    }
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
        }
    }

    private void Awake()
    {
        Bronze = 500;
    }

    private void Update()
    {
        goldText.text = Gold.ToString();
        silverText.text = Silver.ToString();
        bronzeText.text = Bronze.ToString();
    }

    public bool Minus(ScriptableObject scriptable)
    {
        switch (scriptable)
        {
            case Food:
                Food food = scriptable as Food;
                if (Bronze - food.price <  0)
                {
                    if (Silver > (food.price / 1000 ))
                    {
                        Silver -= (food.price / 1000) + 1;
                        Bronze += (1000 - (food.price % 1000));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Bronze -= food.price;
                    return true;
                }
            case Ingredients:
                Ingredients ingredients = scriptable as Ingredients;
                if (Bronze - ingredients.price < 0)
                {
                    if (Silver > (ingredients.price / 1000))
                    {
                        Silver -= (ingredients.price / 1000) + 1;
                        Bronze += (1000 - (ingredients.price % 1000));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Bronze -= ingredients.price;
                    return true;
                }
        }

        return false;
    }
}
