using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : Singleton<MarketUI>
{
    [SerializeField] private Image conversationImageA;
    [SerializeField] private Image charactorImageA;
    [SerializeField] private Image nameA;
    [SerializeField] private Image conversationImageB;
    [SerializeField] private Image charactorImageB;
    [SerializeField] private Image nameB;

    public new void Awake()
    {
        base.Awake(); 
    }
    public void conversationAON()
    {
        conversationImageA.gameObject.SetActive(true);
        nameA.gameObject.SetActive(true);
        conversationBOFF();
        ColorPull(conversationImageA, charactorImageA, nameA);
    }

    public void conversationAOFF()
    {
        conversationImageA.gameObject.SetActive(false);
        nameA.gameObject.SetActive(false);
        ColorHalf(conversationImageA, charactorImageA, nameA);
    }

    public void conversationBON()
    {
        conversationImageB.gameObject.SetActive(true);
        nameB.gameObject.SetActive(true);
        conversationAOFF();
        ColorPull(conversationImageB, charactorImageB, nameB);
    }

    public void conversationBOFF()
    {
        conversationImageB.gameObject.SetActive(false);
        nameB.gameObject.SetActive(false);
        ColorHalf(conversationImageB, charactorImageB, nameB);
    }

    private void ColorPull(Image imageA, Image imageB, Image imageC)
    {
        Color colorA = imageA.color;
        colorA.a = 1f;
        imageA.color = colorA;
        Color colorB = imageB.color;
        colorB.a = 1f;
        imageB.color = colorB;
        Color colorC = imageC.color;
        colorC.a = 1f;
        imageC.color = colorC;
    }
    private void ColorHalf(Image imageA, Image imageB, Image imageC)
    {
        Color colorA = imageA.color;
        colorA.a = 0.5f;
        imageA.color = colorA;
        Color colorB = imageB.color;
        colorB.a = 0.5f;
        imageB.color = colorB;
        Color colorC = imageC.color;
        colorC.a = 0.5f;
        imageC.color = colorC;
    }

}
