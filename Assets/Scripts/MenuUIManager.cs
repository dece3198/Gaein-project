using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public List<Transform> randPosList = new List<Transform>();
    public GameObject startPortal;
    public GameObject exitPortal;
    Vector3 pos = new Vector3(0, 0, 0);
    int rand;
    private void Start()
    {
        rand = Random.Range(0, randPosList.Count);
        startPortal.transform.parent = randPosList[rand];
        startPortal.transform.localPosition = pos;
        randPosList.Remove(randPosList[rand]);
        rand = Random.Range(0, randPosList.Count);
        exitPortal.transform.parent = randPosList[rand];
        exitPortal.transform.localPosition = pos;
    }
}
