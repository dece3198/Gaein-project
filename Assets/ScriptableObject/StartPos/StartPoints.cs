using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoints : MonoBehaviour
{
    [SerializeField] private List<StartPoint> points;
    public List<StartPoint> Points => points;
}
