using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food",menuName ="New Food/Food")]
public class Food : ScriptableObject
{
    public string foodName;
    public Sprite foodImage;
    public GameObject prefab;
}
