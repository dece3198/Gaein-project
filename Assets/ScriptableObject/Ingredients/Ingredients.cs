using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ingredients",menuName = "New Ingredients/Ingredients")]
public class Ingredients : ScriptableObject
{
    public string ingredientsName;
    public Sprite image;
    public float Count;

    public enum Ingredients_TYPE
    {
        Sugar,Salt,Meat,Wheat,Apple,Blueberries,Corn,Onion,Carrot,Egg
    }

    public Ingredients_TYPE ingredientsType;

}
