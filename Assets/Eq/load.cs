using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class load : MonoBehaviour
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
    // Start is called before the first frame update
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
       /* IDataReader selectAllFromPlayer = dbConnector.Select("SELECT * FROM postac");
        while (selectAllFromPlayer.Read())
        {
            Debug.Log("s_1: " + selectAllFromPlayer[0].ToString());
            Debug.Log("s_2: " + selectAllFromPlayer[1].ToString());
            Debug.Log("s_3: " + selectAllFromPlayer[2].ToString());
            Debug.Log("s_4: " + selectAllFromPlayer[3].ToString());
            Debug.Log("s_5: " + selectAllFromPlayer[4].ToString());
            Debug.Log("s_6: " + selectAllFromPlayer[5].ToString());
            Debug.Log("s_7: " + selectAllFromPlayer[6].ToString());
            Debug.Log("s_8: " + selectAllFromPlayer[7].ToString());
            Debug.Log("money: " + selectAllFromPlayer[8].ToString());
            Debug.Log("moneytoadd: " + selectAllFromPlayer[9].ToString());
            Debug.Log("hp: " + selectAllFromPlayer[10].ToString());
            Debug.Log("armor: " + selectAllFromPlayer[11].ToString());
            Debug.Log("str: " + selectAllFromPlayer[12].ToString());
            Debug.Log("vit: " + selectAllFromPlayer[13].ToString());
            Debug.Log("dex: " + selectAllFromPlayer[14].ToString());
            Debug.Log("luck: " + selectAllFromPlayer[15].ToString());
            
            textmeshpro_text_text.text = selectAllFromPlayer[10].ToString();
            textmeshpro_text_text1.text = selectAllFromPlayer[12].ToString();
            textmeshpro_text_text2.text = selectAllFromPlayer[14].ToString();
            textmeshpro_text_text3.text = selectAllFromPlayer[11].ToString();
            textmeshpro_text_text4.text = selectAllFromPlayer[13].ToString();
            textmeshpro_text_text5.text = selectAllFromPlayer[15].ToString();
       
        }

        Debug.Log(selectAllFromPlayer.ToString());
       */
    }
    public void Update()
    {
        // IDataReader selectAllFromPlayer = Select("SELECT * FROM Testowa");
        IDataReader selectAllFromPlayer = dbConnector.Select("SELECT * FROM postac");
        while (selectAllFromPlayer.Read())
        {/*
            Debug.Log("s_1: " + selectAllFromPlayer[0].ToString());
            Debug.Log("s_2: " + selectAllFromPlayer[1].ToString());
            Debug.Log("s_3: " + selectAllFromPlayer[2].ToString());
            Debug.Log("s_4: " + selectAllFromPlayer[3].ToString());
            Debug.Log("s_5: " + selectAllFromPlayer[4].ToString());
            Debug.Log("s_6: " + selectAllFromPlayer[5].ToString());
            Debug.Log("s_7: " + selectAllFromPlayer[6].ToString());
            Debug.Log("s_8: " + selectAllFromPlayer[7].ToString());
            Debug.Log("money: " + selectAllFromPlayer[8].ToString());
            Debug.Log("moneytoadd: " + selectAllFromPlayer[9].ToString());
            Debug.Log("hp: " + selectAllFromPlayer[10].ToString());
            Debug.Log("armor: " + selectAllFromPlayer[11].ToString());
            Debug.Log("str: " + selectAllFromPlayer[12].ToString());
            Debug.Log("vit: " + selectAllFromPlayer[13].ToString());
            Debug.Log("dex: " + selectAllFromPlayer[14].ToString());
            Debug.Log("luck: " + selectAllFromPlayer[15].ToString());
            */
            string ab = selectAllFromPlayer[10].ToString();
            int a1 = Int32.Parse(ab);
            string ah = selectAllFromPlayer[13].ToString();
            int a2 = Int32.Parse(ah);
            int a3 = a1 * a2;
            string hp = a3.ToString();

            textmeshpro_text_text.text = "Hp: " + hp;

            textmeshpro_text_text1.text = "Str: " + selectAllFromPlayer[12].ToString();
            textmeshpro_text_text2.text = "Dex: " + selectAllFromPlayer[14].ToString();
            textmeshpro_text_text3.text = "Armor: " + selectAllFromPlayer[11].ToString();
            textmeshpro_text_text4.text = "Vit: " + selectAllFromPlayer[13].ToString();
            textmeshpro_text_text5.text = "Luck: " + selectAllFromPlayer[15].ToString();
        }

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
