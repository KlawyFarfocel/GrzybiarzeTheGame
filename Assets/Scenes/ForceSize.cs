using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Zmien rozmiar logo
        RectTransform logo = GameObject.Find("GameLogo").GetComponent<RectTransform>();
        logo.sizeDelta = new Vector2(150, 150);

        //Znajdz panel i canvas [do skalowania]
        RectTransform panel = GameObject.Find("Panel").GetComponent<RectTransform>();
        Canvas canvas = GameObject.Find("Las").GetComponent<Canvas>();


        //zmien rozmiar emerytkaUI i levelUI i je przestaw
        RectTransform emerytkaUI= GameObject.Find("EmerytkaUI").GetComponent<RectTransform>();

        float panelWidth=panel.rect.width;
        Debug.Log(panelWidth);
        float UIWidth = (panelWidth - 150) / 2;
        float UIX = UIWidth / 2;

        emerytkaUI.sizeDelta = new Vector2(UIWidth,150);
        emerytkaUI.anchoredPosition = new Vector2(UIX, -75);
        RectTransform LevelUI = GameObject.Find("LevelUI").GetComponent<RectTransform>();
        LevelUI.sizeDelta = new Vector2(UIWidth, 150);
        LevelUI.anchoredPosition = new Vector2(-UIX, -75);

        //ustaw gameLogo na srodek
        RectTransform GameLogo = GameObject.Find("GameLogo").GetComponent<RectTransform>();
        GameLogo.anchoredPosition=new Vector2(panelWidth / 2, -75);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
