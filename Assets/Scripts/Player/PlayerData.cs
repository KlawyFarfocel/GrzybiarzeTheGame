using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private DBConnector dbConnector;
    public GameObject player;
    void Start()
    {
        dbConnector = GameObject.Find("LoadPlayerStats").GetComponent<DBConnector>();

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

            Player playerData = player.GetComponent<Player>();

            playerData.HP = HP;
            playerData.ARMOR = ARMOR;
            playerData.STR = STR;
            playerData.VIT = VIT;
            playerData.DEX = DEX;
            playerData.LUCK = LUCK;

        }
    }

}
