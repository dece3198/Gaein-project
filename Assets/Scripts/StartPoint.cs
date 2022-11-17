using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    GameObject player;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        if (player != null)
        {
            player.transform.localPosition = transform.position;
        }
    }
}
