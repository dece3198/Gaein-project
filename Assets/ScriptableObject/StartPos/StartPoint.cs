using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New StartPosition", menuName = "New StartPosition/StartPosition")]
public class StartPoint : ScriptableObject
{
    public string posName;
    public Vector3 startPos;
}
