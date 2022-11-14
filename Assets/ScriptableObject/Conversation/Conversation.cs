using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Conversation")]
public class Conversation : ScriptableObject
{
    [System.Serializable]
    public class Talk
    {
        public string talkerName;
        [SerializeField, TextArea(2, 5)]
        public string content;
        public Sprite portrait;
    }

    [SerializeField]
    private Talk[] _talks;
    public Talk[] talks { get { return _talks; } }

    [SerializeField]
    private Conversation _nextConversation;
    public Conversation nextConversation { get { return _nextConversation; } }
}
