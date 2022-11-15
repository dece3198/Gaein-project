using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Conversation_TYPE
{
    NPC, Player
}

public enum NPC_TYPE
{
    Store,NotStore
}

public enum STORE_TYPE
{
    ButcherShop, VegetableShop
}

[System.Serializable]
public class Talk
{
    public string talkerName;
    [SerializeField, TextArea(2, 5)]
    public string content;
    public Sprite portrait;

    public Conversation_TYPE type;
}

[CreateAssetMenu(menuName = "ScriptableObject/Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField]
    private Talk[] _talks;
    public Talk[] talks { get { return _talks; } }
    [SerializeField]
    private Conversation _nextConversation;
    public Conversation nextConversation { get { return _nextConversation; } }

    public NPC_TYPE npcType;
    public STORE_TYPE storeType;
}
