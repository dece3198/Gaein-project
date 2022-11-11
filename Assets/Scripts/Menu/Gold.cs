using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Gold : MonoBehaviour
{
    public static Gold Instance;


    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI silver;
    [SerializeField] private TextMeshProUGUI bronze;

    public int goldValue = 0;
    public int silverValue = 0;
    public int bronzeValue = 0;


    private void Awake()
    {
        Instance = this;
        bronzeValue = 100;
    }

    private void Update()
    {
        gold.text = goldValue.ToString();
        silver.text = silverValue.ToString();
        bronze.text = bronzeValue.ToString();
    }

    public bool Minus(Food food)
    {
        switch (food.goldType)
        {
            case Food.GOLD_TYPE.Gold : return GoldCheck(food);
            case Food.GOLD_TYPE.Silver: return SilverCheck(food);
            case Food.GOLD_TYPE.Bronze: return BronzeCheck(food);
        }  

        return false;
    }

    private bool GoldCheck(Food food)
    {
        if(goldValue < food.Price)
        {
            return false;
        }
        else
        {
            goldValue -= food.Price;
        }

        return true;
    }

    public bool SilverCheck(Food food)
    {
        if(goldValue > 0)
        {
            if(silverValue < food.Price)
            {
                goldValue -= 1;
                silverValue += 1000 - food.Price;
                return true;
            }
            else
            {
                silverValue -= food.Price;
                return true;
            }
        }
        else
        {
            if(silverValue >= food.Price)
            {
                silverValue -= food.Price;
                return true;
            }
        }
        return false;
    }

    public bool BronzeCheck(Food food)
    {
        if (silverValue > 0)
        {
            if (bronzeValue < food.Price)
            {
                silverValue -= 1;
                bronzeValue += 1000 - food.Price;
                return true;
            }
        }
        else
        {
            if (bronzeValue > food.Price)
            {
                bronzeValue -= food.Price;
                return true;
            }
        }
        return false;
    }

}
