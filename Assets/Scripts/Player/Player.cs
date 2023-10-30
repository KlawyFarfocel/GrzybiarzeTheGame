using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Player : MonoBehaviour
{
    public int HP;
    public int CURRENT_HP;
    public int ARMOR;
    public int STR;
    public int VIT;
    public int DEX;
    public int LUCK;

    public void HandleUIChange()
    {
        TextMeshProUGUI UIHealthText = GameObject.Find("PlayerHP").GetComponent<TextMeshProUGUI>();
        UIHealthText.text = $"{CURRENT_HP}/{HP}";
        LayoutElement HPCurrent = GameObject.Find("HPCurrent").GetComponent<LayoutElement>();
        float hpPercentage = (float)CURRENT_HP / HP;
        hpPercentage = (float)Math.Round(hpPercentage, 2);
        HPCurrent.flexibleWidth = hpPercentage;

        if(hpPercentage<0.5)
        {
            UnityEngine.UI.Image HPCurrentBar = GameObject.Find("HPCurrent").GetComponent<UnityEngine.UI.Image>();
            if (hpPercentage > 0.25)
            {
                HPCurrentBar.color = ColorUtility.TryParseHtmlString("#FF6E00", out Color parsedColor) ? parsedColor : Color.white;
            }
            else
            {
                HPCurrentBar.color = ColorUtility.TryParseHtmlString("#8B0000", out Color parsedColor) ? parsedColor : Color.white;
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
            CURRENT_HP-=health;
        }
        HandleUIChange();
    }
}