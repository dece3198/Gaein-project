using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T _instance;
    public static T Instance
    {
        get
        {
            //if( _instance == null )
            //{
            //    GameObject obj;
            //    obj = GameObject.Find(typeof(T).Name);
            //    if (obj == null)
            //    {
            //        Debug.Log("함수 생성");
            //        obj = new GameObject(typeof(T).Name);
            //        _instance = obj.AddComponent<T>();
            //    }
            //    else
            //    {
            //        _instance = obj.GetComponent<T>();
            //    }
            //}
            return _instance; 
        }
    }

    public void Awake()
    {
        if(_instance == null)
        {
            _instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != null)
        {
            Destroy(this.gameObject);
        }

    }
}
