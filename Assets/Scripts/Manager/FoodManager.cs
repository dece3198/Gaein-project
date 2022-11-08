using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance;
    public List<Ingredients> ingredients = new List<Ingredients>();
    public List<Food> foodList = new List<Food>();

    private void Awake()
    {
        instance = this;
    }

    public void Cooking(Food food)
    {

    }

    IEnumerator CookintCo()
    {
        yield return new WaitForSeconds(10);
    }
}
