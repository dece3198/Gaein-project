using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Food;

public class FoodManager : Singleton<FoodManager>
{
    public List<Ingredients> ingredients = new List<Ingredients>();
    public List<Food> foodList = new List<Food>();
    public List<Transform> transforms = new List<Transform>();
    public List<GameObject> stews = new List<GameObject>();
    public List<GameObject> applePie = new List<GameObject>();
    public List<GameObject> beer = new List<GameObject>();

    Dictionary<FOOD_TYPE, List<GameObject>> poolDic = new Dictionary<Food.FOOD_TYPE, List<GameObject>>();
    Dictionary<FOOD_TYPE, float> foodTime = new Dictionary<FOOD_TYPE, float>();
    private int foodNumber = 0;

    public new void Awake()
    {
        base.Awake();
        poolDic.Add(Food.FOOD_TYPE.Stew, stews);
        poolDic.Add(Food.FOOD_TYPE.ApplePie, applePie);
        poolDic.Add(Food.FOOD_TYPE.Beer, beer);
        foodTime.Add(FOOD_TYPE.Stew, 15f);
        foodTime.Add(FOOD_TYPE.ApplePie, 15f);
        foodTime.Add(FOOD_TYPE.Beer, 5f);
    }

    private void Start()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                GameObject gameObject = Instantiate(foodList[i].prefab);
                switch (i)
                {
                    case 0 : stews.Add(gameObject); break;
                    case 1 : beer.Add(gameObject); break;
                    case 2 : applePie.Add(gameObject); break;
                }
                gameObject.transform.parent = transform;
                gameObject.SetActive(false);
            }
        }
    }

    //�մ����׼� �ֹ��� ����
    public void Cooking(Food food)
    {
        if(food.type == TYPE.Alcohol)
        {
            StartCoroutine(CookintCo(food));
            return;
        }

        for(int i = 0; i < food.recipe.Count; i++)
        {
            for(int j = 0; j < ingredients.Count; j++)
            {
                if (food.recipe[i] == ingredients[j])
                {
                    if(ingredients[j].Count < 0.2f)
                    {
                        StartCoroutine(CookintCo(foodList[1]));
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < food.recipe.Count; i++)
        {
            for (int j = 0; j < ingredients.Count; j++)
            {
                if (food.recipe[i] == ingredients[j])
                {
                    ingredients[j].Count -= 0.2f;
                }
            }
        }

        StartCoroutine(CookintCo(food));
    }
    public void EnterPool(Food.FOOD_TYPE foodType,GameObject intputObj)
    {
        intputObj.SetActive(false);
        intputObj.transform.parent = transform;
        poolDic[foodType].Add(intputObj);
    }

    //�ֹ��� ������ 15�� �ڿ� �˸´� �丮Ÿ���� �Լ��� �����
    IEnumerator CookintCo(Food food)
    {
        GoldManager.Instance.bronze += food.price;
        yield return new WaitForSeconds(foodTime[food.foodType]);
        switch (food.foodType)
        {
            case Food.FOOD_TYPE.Stew : FoodType(stews, 0); break;
            case Food.FOOD_TYPE.Beer: FoodType(beer, 1); break;
            case Food.FOOD_TYPE.ApplePie: FoodType(applePie, 2); break;
        }
    }
    public void Drink(Food food)
    {
        StartCoroutine(CookintCo(food));
    }

    //�ֹ��� �丮�� ������ ��ġ�� ������ ��
    private GameObject FoodType(List<GameObject> list, int number)
    {
        if(list.Count <= foodNumber)
        {
            Refill(list, number);
        }
        int rand = Random.Range(0, transforms.Count);
        GameObject getFood = list[0];
        list.Remove(list[0]);
        getFood.SetActive(true);
        getFood.transform.position = transforms[rand].position;
        foodNumber++;
        return getFood;
    }

    //������ �丮�� �����ϸ� ����
    private void Refill(List<GameObject> list, int number)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject gameObject = Instantiate(foodList[number].prefab);
            list.Add(gameObject);
            gameObject.transform.parent = transform;
            gameObject.SetActive(false);
        }
    }



}
