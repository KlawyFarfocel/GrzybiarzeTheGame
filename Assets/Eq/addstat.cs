using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using TMPro;
using UnityEngine;

public class addStat : MonoBehaviour
{
    DBConnector dbConnector;
    Player player;
    void Start()
    {
        dbConnector = GameObject.Find("TopPanel").GetComponent<DBConnector>();
        player = GameObject.Find("LevelManager").GetComponent<Player>();
    }
    public void AddStat(string stat)
    {
        IDataReader selectMoneyAndCostQuery = dbConnector.Select($"SELECT money,{stat}_c FROM postac JOIN click");
        int money=-1, upgradeCost=0;
        while ( selectMoneyAndCostQuery.Read() ) {  
            money = int.Parse(selectMoneyAndCostQuery[0].ToString());
            upgradeCost = int.Parse(selectMoneyAndCostQuery[1].ToString());
        }
        if (money >= upgradeCost)
        {
            money -= upgradeCost;
            upgradeCost += 10;
            if (stat == "str") player.STR++;
            else if (stat == "vit") player.VIT++;
            else if (stat == "dex") player.DEX++;
            else player.LUCK++;

            dbConnector.UpdateDB($"UPDATE postac SET money={money}");
            dbConnector.UpdateDB($"UPDATE click SET {stat}_c={upgradeCost}");
        }
        GameObject.Find("Main").GetComponent<Load>().ForceUpdateStatTexts();
    }
}
