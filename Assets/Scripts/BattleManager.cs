using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject player;
    public Transform startPos;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        player.transform.localPosition = startPos.position;
    }
}
