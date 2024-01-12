using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    
    private DBConnector dbConnector;

    //hp
    public GameObject textmeshpro_text;
    TextMeshProUGUI textmeshpro_text_text;
    //str
    public GameObject textmeshpro_text1;
    TextMeshProUGUI textmeshpro_text_text1;
    //dex
    public GameObject textmeshpro_text2;
    TextMeshProUGUI textmeshpro_text_text2;
    //armor
    public GameObject textmeshpro_text3;
    TextMeshProUGUI textmeshpro_text_text3;
    //vit
    public GameObject textmeshpro_text4;
    TextMeshProUGUI textmeshpro_text_text4;
    //luck
    public GameObject textmeshpro_text5;
    TextMeshProUGUI textmeshpro_text_text5;
    //str_c
    public GameObject textmeshpro_text6;
    TextMeshProUGUI textmeshpro_text_text6;
    //dex_c
    public GameObject textmeshpro_text7;
    TextMeshProUGUI textmeshpro_text_text7;
    //vir_c
    public GameObject textmeshpro_text8;
    TextMeshProUGUI textmeshpro_text_text8;
    //luck_c
    public GameObject textmeshpro_text9;
    TextMeshProUGUI textmeshpro_text_text9;


    Player playerData;
    private IEnumerator setHPBar()
    {
        yield return new WaitForSeconds(0.3f);


    }
    public void Start()
    {
        textmeshpro_text_text = textmeshpro_text.GetComponent<TextMeshProUGUI>();
        textmeshpro_text_text1 = textmeshpro_text1.GetComponent<TextMeshProUGUI>();
        textmeshpro_text_text2 = textmeshpro_text2.GetComponent<TextMeshProUGUI>();
        textmeshpro_text_text3 = textmeshpro_text3.GetComponent<TextMeshProUGUI>();
        textmeshpro_text_text4 = textmeshpro_text4.GetComponent<TextMeshProUGUI>();
        textmeshpro_text_text5 = textmeshpro_text5.GetComponent<TextMeshProUGUI>();
        textmeshpro_text_text6 = textmeshpro_text6.GetComponent<TextMeshProUGUI>();
        textmeshpro_text_text7 = textmeshpro_text7.GetComponent<TextMeshProUGUI>();
        textmeshpro_text_text8 = textmeshpro_text8.GetComponent<TextMeshProUGUI>();
        textmeshpro_text_text9 = textmeshpro_text9.GetComponent<TextMeshProUGUI>();

        dbConnector = GameObject.Find("TopPanel").GetComponent<DBConnector>();

        playerData = GameObject.Find("LevelManager").GetComponent<Player>();

        EvaluateHPPoints();
        ForceUpdateStatTexts();
    }
    public void EvaluateHPPoints()
    {
        int maxHP = playerData.VIT * 10;
        int playerHP = playerData.CURRENT_HP;
        if(playerHP>maxHP) playerData.CURRENT_HP = maxHP;
        float percentage = (float)Math.Round((playerHP / (float)maxHP), 2);

        GameObject.Find("CurrentHp").GetComponent<LayoutElement>().flexibleWidth = percentage;
        GameObject.Find("MaxHp").GetComponent<LayoutElement>().flexibleWidth = 1 - percentage;
        textmeshpro_text_text.text = $"HP: {playerHP}/{maxHP} ({percentage * 100}%)";
    }
    public void ForceUpdateStatTexts()
    {
        EvaluateHPPoints();
        textmeshpro_text_text1.text = "Si³a: " + playerData.STR.ToString();
        textmeshpro_text_text2.text = "Zrêcznoœæ: " + playerData.DEX.ToString();
        textmeshpro_text_text3.text = "Pancerz: " + playerData.ARMOR.ToString();
        textmeshpro_text_text4.text = "Witalnoœæ: " + playerData.VIT.ToString();
        textmeshpro_text_text5.text = "Szczêœcie: " + playerData.LUCK.ToString();

        IDataReader selectAllFromPlayer1 = dbConnector.Select("SELECT * FROM click");
        while (selectAllFromPlayer1.Read())
        {
            textmeshpro_text_text6.text = "Cena: " + selectAllFromPlayer1[1].ToString();
            textmeshpro_text_text7.text = "Cena: " + selectAllFromPlayer1[0].ToString();
            textmeshpro_text_text8.text = "Cena: " + selectAllFromPlayer1[2].ToString();
            textmeshpro_text_text9.text = "Cena: " + selectAllFromPlayer1[3].ToString();

        }
    }
}
