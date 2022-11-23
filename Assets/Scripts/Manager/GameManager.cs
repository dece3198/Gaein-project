using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<StartPoint> startPointList = new List<StartPoint>();
    Dictionary<string, Vector3> startPositionDic = new Dictionary<string, Vector3>();
    public GameObject player;

    private new void Awake()
    {
        base.Awake();
        for (int i = 0; i < startPointList.Count; i++)
        {
            startPositionDic.Add(startPointList[i].posName, startPointList[i].startPos);
        }
    }

    private void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<PlayerController>().gameObject;
            SceneManager.sceneLoaded += SetPlayerPosition;
        }
    }

    public void SetPlayerPosition(Scene scen, LoadSceneMode mode)
    {
        if(scen.name == "Game")
        {
            player.transform.localPosition = startPositionDic["Game"];
        }
        Debug.Log(scen.name);
        player.transform.localPosition = startPositionDic[scen.name];
    }
}
