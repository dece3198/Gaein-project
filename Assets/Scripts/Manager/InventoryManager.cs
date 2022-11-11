using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> Count = new List<TextMeshProUGUI>();
    [SerializeField] private List<Ingredients> Ingredients = new List<Ingredients>();


    private void Update()
    {
        for(int i = 0; i < Count.Count; i++)
        {
            for (int j = 0; j < Ingredients.Count; j++)
            {
                if (Count[i].transform.parent.GetComponent<Image>().sprite == Ingredients[j].image)
                {
                    Count[i].text = Ingredients[i].Count.ToString();
                }
            }
        }
    }
}
