using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct ConversationStruct
{
    public Image conversationImage;
    public TextMeshProUGUI textUI;
    public TextMeshProUGUI nameUI;

    public void Set(Talk talk)
    {
        conversationImage.sprite = talk.portrait;
        textUI.text = talk.content;
        nameUI.text = talk.talkerName;
    }
}

public class ConversationController : Singleton<ConversationController>
{
    [SerializeField] private Button yesBut;
    [SerializeField] private Button noBut;
    [SerializeField] private Canvas butcherShop;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private int currentIndex = 0;
    public bool isShopOn = true;

    public Canvas canvas;
    public ConversationStruct playerConversationStruct;
    public ConversationStruct npcConversationStruct;
    
    public Dictionary<Conversation_TYPE, ConversationStruct> talkDic = new Dictionary<Conversation_TYPE, ConversationStruct>();
    public Dictionary<STORE_TYPE, Canvas> shopDic = new Dictionary<STORE_TYPE, Canvas>();
    Animator animator;
    Talk[] talks;
    private void Awake()
    {
        talkDic.Add(Conversation_TYPE.Player, playerConversationStruct);
        talkDic.Add(Conversation_TYPE.NPC, npcConversationStruct);
        shopDic.Add(STORE_TYPE.ButcherShop, butcherShop);
        animator = canvas.GetComponent<Animator>();
        butcherShop.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        yesBut.gameObject.SetActive(false);
        noBut.gameObject.SetActive(false);
    }
    public void OnTalk(Conversation conversation)
    {

        if (talks == null)
        {
            talks = conversation.talks;
        }

        currentIndex++;

        if (currentIndex > talks.Length)
        {
            if(conversation.nextConversation != null)
            {
                talks = conversation.nextConversation.talks;
            }

            if (conversation.npcType == NPC_TYPE.Store)
            {
                Cursor.lockState = CursorLockMode.None;
                yesBut.gameObject.SetActive(true);
                noBut.gameObject.SetActive(true);
                yesBut.onClick.AddListener(()=> { YesClick(conversation); });
                noBut.onClick.AddListener(NoClick);
            }
            currentIndex = 0;
            return;
        }

        Talk talk = talks[currentIndex-1];
        talkDic[talk.type].Set(talk);

        if (talk.type == Conversation_TYPE.NPC)
        {
            if((currentIndex -1) <= 0)
            {
                playerRenderer.sortingOrder = -1;
                animator.Play("UI_A");
                return;
            }
            playerRenderer.sortingOrder = -1;
            animator.SetTrigger("UI_B_1");
        }
        if (talk.type == Conversation_TYPE.Player)
        {
            if ((currentIndex - 1) <= 0)
            {
                playerRenderer.sortingOrder = 0;
                animator.Play("UI_B");
                return;
            }
            playerRenderer.sortingOrder = 0;
            animator.SetTrigger("UI_A_1");
        }
    }

    private void Update()
    {
        /*
        if(yesBut.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                NoClick();
            }
        }
        */
    }

    public void YesClick(Conversation conversation)
    {
        isShopOn = false;
        canvas.gameObject.SetActive(false);
        shopDic[conversation.storeType].gameObject.SetActive(true);
        yesBut.gameObject.SetActive(false);
        currentIndex = 0;
    }

    public void NoClick()
    {
        isShopOn = true;
        currentIndex = 0;
        canvas.gameObject.SetActive(false);
        yesBut.gameObject.SetActive(false);
        noBut.gameObject.SetActive(false);
        butcherShop.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
