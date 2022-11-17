using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUI : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            ConversationController.Instance.NoClick();
        }
    }
}
