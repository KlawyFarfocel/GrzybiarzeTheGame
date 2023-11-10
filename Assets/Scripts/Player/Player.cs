using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public int HP;
    public int CURRENT_HP;
    public int ARMOR;
    public int STR;
    public int VIT;
    public int DEX;
    public int LUCK;

    private static Player instance;
    private DBConnector dbConnector;
    private eqManager eqManager;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void Start()
    {
        LoadPlayerData();
    }

    public void HandleUIChange()
    {
        TextMeshProUGUI UIHealthText = GameObject.Find("PlayerHP").GetComponent<TextMeshProUGUI>();
        UIHealthText.text = $"{CURRENT_HP}/{HP}";
        LayoutElement HPCurrent = GameObject.Find("HPCurrent").GetComponent<LayoutElement>();
        float hpPercentage = (float)CURRENT_HP / HP;
        hpPercentage = (float)Math.Round(hpPercentage, 2);
        HPCurrent.flexibleWidth = hpPercentage;

        if (hpPercentage < 0.5)
        {
            UnityEngine.UI.Image HPCurrentBar = GameObject.Find("HPCurrent").GetComponent<UnityEngine.UI.Image>();
            if (hpPercentage > 0.25)
            {
                HPCurrentBar.color = UnityEngine.ColorUtility.TryParseHtmlString("#FF6E00", out Color parsedColor) ? parsedColor : Color.white;
            }
            else
            {
                HPCurrentBar.color = UnityEngine.ColorUtility.TryParseHtmlString("#8B0000", out Color parsedColor) ? parsedColor : Color.white;
            }

        }

        LayoutElement HPLeft = GameObject.Find("HpLeft").GetComponent<LayoutElement>();
        float hpLeft = 1 - hpPercentage;
        HPLeft.flexibleWidth = hpLeft;
    }
    public void HandleHealthLoss(int health)
    {
        if (health > 0)
        {
            CURRENT_HP -= health;
        }
        HandleUIChange();
    }

    public void LoadPlayerData()
    {
        dbConnector = GameObject.Find("Las").GetComponent<DBConnector>();
        IDataReader SelectPlayer = dbConnector.Select("SELECT hp,armor,str,vit,dex,luck FROM postac");
        while (SelectPlayer.Read())
        {
            string hp = SelectPlayer[0].ToString();
            int HP = Int32.Parse(hp);
            string armor = SelectPlayer[1].ToString();
            int ARMOR = Int32.Parse(armor);
            string str = SelectPlayer[2].ToString();
            int STR = Int32.Parse(str);
            string vit = SelectPlayer[3].ToString();
            int VIT = Int32.Parse(vit);
            string dex = SelectPlayer[4].ToString();
            int DEX = Int32.Parse(dex);
            string luck = SelectPlayer[5].ToString();
            int LUCK = Int32.Parse(luck);


            this.HP = HP * VIT * 10;
            this.CURRENT_HP = HP * VIT * 10;
            this.ARMOR = ARMOR;
            this.STR = STR;
            this.VIT = VIT;
            this.DEX = DEX;
            this.LUCK = LUCK;
            this.HandleHealthLoss(0); //ustawia tekst ui zeby bylo dobrze
        }
    }
    public void AddStats(eqItem item)
    {
        if (item.armor != 0 ) { this.ARMOR += item.armor; }
        if (item.mod_1 != 0) { this.VIT += item.mod_1_val; }
        if (item.mod_2 != 0) { this.DEX += item.mod_2_val; }
        if (item.mod_3 != 0) { this.STR += item.mod_3_val; }
        if (item.mod_4 != 0) { this.LUCK += item.mod_4_val; }
    }
    public void RemoveStats(eqItem item)
    {
        if (item.armor != 0) { this.ARMOR -= item.armor; }
        if (item.mod_1 != 0) { this.VIT -= item.mod_1_val; }
        if (item.mod_2 != 0) { this.DEX -= item.mod_2_val; }
        if (item.mod_3 != 0) { this.STR -= item.mod_3_val; }
        if (item.mod_4 != 0) { this.LUCK -= item.mod_4_val; }
    } 
}