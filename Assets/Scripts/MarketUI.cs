using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour
{
    Animator animator;
    [SerializeField] private Image conversationImageA;
    [SerializeField] private Image conversationImageB;
    [SerializeField] private Image charactorImageA;
    [SerializeField] private Image charactorImageB;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void conversationAON()
    {
        conversationImageA.gameObject.SetActive(true);
        Color colorA = conversationImageA.color;
        colorA.a = 255;
        conversationImageA.color = colorA;
        Color colorB = charactorImageA.color;
        colorB.a = 255;
        charactorImageA.color = colorB;
        conversationBOFF();
    }

    public void conversationAOFF()
    {
        conversationImageA.gameObject.SetActive(false);
        Color colorA = conversationImageA.color;
        colorA.a = 1;
        conversationImageA.color = colorA;
        Color colorB = charactorImageA.color;
        colorB.a = 1;
        charactorImageA.color = colorB;
    }

    public void conversationBON()
    {
        conversationImageB.gameObject.SetActive(true);
        Color colorA = conversationImageB.color;
        colorA.a = 255;
        conversationImageB.color = colorA;
        Color colorB = charactorImageB.color;
        colorB.a = 255;
        charactorImageB.color = colorB;
        conversationAOFF();
    }

    public void conversationBOFF()
    {
        conversationImageB.gameObject.SetActive(false);
        Color colorA = conversationImageB.color;
        colorA.a = 1;
        conversationImageB.color = colorA;
        Color colorB = charactorImageB.color;
        colorB.a = 1;
        charactorImageB.color = colorB;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("UI_A");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.Play("UI_A_1");

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play("UI_B");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.Play("UI_B_1");
        }
    }
}
