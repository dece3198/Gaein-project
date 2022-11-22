using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject player;
    public Transform startPos;
    bool isPlayer = true;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        player.transform.localPosition = startPos.position;
    }

    private void Update()
    {
        if (player == null)
        {
            isPlayer = true;
            player = FindObjectOfType<PlayerController>().gameObject;
        }
        else
        {
            if (isPlayer)
            {
                isPlayer = false;
                player.transform.localPosition = startPos.position;
            }
        }
    }
}
