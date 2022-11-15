using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour, IInteraction
{
    [SerializeField] private Conversation conversation;
    public ConversationController conversationController;
    private void Awake()
    {
        conversationController = FindObjectOfType<ConversationController>();
    }
    public void Interaction()
    {
        if (conversation != null)
        {
            conversationController.OnTalk(conversation);
        }
    }
}
