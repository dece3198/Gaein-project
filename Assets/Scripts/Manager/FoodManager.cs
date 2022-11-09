using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance;
    public List<Ingredients> ingredients = new List<Ingredients>();
    public List<Food> foodList = new List<Food>();
    public List<Transform> transforms = new List<Transform>();
    public List<GameObject> stews = new List<GameObject>();
    public List<GameObject> applePie = new List<GameObject>();
    public List<GameObject> beer = new List<GameObject>();

    private int foodNumber = 0;

    private void Awake()
    {
        instance = this;
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

    //손님한테서 주문을 받음
    public void Cooking(Food food)
    {
        StartCoroutine(CookintCo(food));
    }

    //주문을 받으면 15초 뒤에 알맞는 요리타입의 함수가 실행됨
    IEnumerator CookintCo(Food food)
    {
        yield return new WaitForSeconds(15f);
        switch (food.foodType)
        {
            case Food.FOOD_TYPE.Stew : FoodType(stews, 0); break;
            case Food.FOOD_TYPE.Beer: FoodType(beer, 1); break;
            case Food.FOOD_TYPE.ApplePie: FoodType(applePie, 2); break;
        }
    }
    public void Drink(Food food)
    {
        StartCoroutine(DrinkCo(food));
    }

    IEnumerator DrinkCo(Food food)
    {
        yield return new WaitForSeconds(3f);
        switch (food.foodType)
        {
            case Food.FOOD_TYPE.Beer: FoodType(beer, 1); break;
        }
    }

    //주문한 요리가 랜덤한 위치에 생성이 됨
    private void FoodType(List<GameObject> list, int number)
    {
        if(list.Count <= foodNumber)
        {
            Refill(list, number);
        }
        int rand = Random.Range(0, transforms.Count);
        list[foodNumber].SetActive(true);
        list[foodNumber].transform.position = transforms[rand].position;
        foodNumber++;
    }

    //생성될 요리가 부족하면 리필
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
