using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject player;
    public Transform startPos;


    private void Start()
    {
        Debug.Log("µé¾î¿È");
        player = FindObjectOfType<PlayerController>().gameObject;
        player.gameObject.transform.localPosition = startPos.position;
    }

}
