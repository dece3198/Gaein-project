using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Transform startPos;
    bool isPlayer = true;

    private void Update()
    {
        if (player == null)
        {
            isPlayer = true;
            player = FindObjectOfType<PlayerController>().gameObject;
        }
        else
        {
            if(isPlayer)
            {
                isPlayer = false;
                player.transform.localPosition = startPos.position;
            }
        }
    }

}
