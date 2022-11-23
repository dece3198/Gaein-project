using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestoryObject : MonoBehaviour
{ 
    private void Awake()
    {
        var objs = FindObjectsOfType<DontDestoryObject>();

        if(objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
