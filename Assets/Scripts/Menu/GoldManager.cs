using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : Singleton<GoldManager>
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI silverText;
    public TextMeshProUGUI bronzeText;
    public int gold;
    public int silver;
    public int bronze;


    private void Awake()
    {
        bronze = 100;
    }

    private void Update()
    {
        goldText.text = gold.ToString();
        silverText.text = silver.ToString();
        bronzeText.text = bronze.ToString();
        GoldUpdate();
    }

    private void GoldUpdate()
    {
        if(silver > 999)
        {
            gold += 1;
            silver -= 1000;
        }
        if(bronze > 999)
        {
            silver += 1;
            bronze -= 1000;
        }
    }

    public void Plus(Food food)
    {
        switch (food.goldType)
        {
            case Food.GOLD_TYPE.Gold: gold += food.Price; break;
            case Food.GOLD_TYPE.Silver: silver += food.Price; break;
            case Food.GOLD_TYPE.Bronze: bronze += food.Price; break;
        }
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
        if(gold < food.Price)
        {
            return false;
        }
        else
        {
            gold -= food.Price;
        }

        return true;
    }

    public bool SilverCheck(Food food)
    {
        if(gold > 0)
        {
            if(silver < food.Price)
            {
                gold -= 1;
                silver += 1000 - food.Price;
                return true;
            }
            else
            {
                silver -= food.Price;
                return true;
            }
        }
        else
        {
            if(silver >= food.Price)
            {
                silver -= food.Price;
                return true;
            }
        }
        return false;
    }

    public bool BronzeCheck(Food food)
    {
        if (silver > 0)
        {
            if (bronze < food.Price)
            {
                silver -= 1;
                bronze += 1000 - food.Price;
                return true;
            }
        }
        else
        {
            if (bronze > food.Price)
            {
                bronze -= food.Price;
                return true;
            }
        }
        return false;
    }

}
