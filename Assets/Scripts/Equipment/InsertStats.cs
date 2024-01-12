using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class InsertStats : MonoBehaviour
{
    public DBConnector connector;
    public Player player;
    public eqManager eqManager;
    private void Start()
    {
        connector = this.GetComponent<DBConnector>();
        player = GameObject.Find("LevelManager").GetComponent<Player>();
    }
    void OnDestroy()
    {
        List<int> equippedItems = eqManager.equippedItemsIdList;
        string queryString = $"UPDATE postac SET {(equippedItems[0] == -1 ? "s_1=NULL" : "s_1="+equippedItems[0])},{(equippedItems[1] == -1 ? "s_2=NULL" : "s_2="+equippedItems[1])}, {(equippedItems[2] == -1 ? "s_3=NULL" : "s_3="+equippedItems[2])}, {(equippedItems[3] == -1 ? "s_4=NULL" : "s_4="+equippedItems[3])}, {(equippedItems[4] == -1 ? "s_5=NULL" : "s_5=" + equippedItems[4])}, {(equippedItems[5] == -1 ? "s_6=NULL" : "s_6="+equippedItems[5])},{(equippedItems[6] == -1 ? "s_7=NULL" : "s_7="+equippedItems[6])}, {(equippedItems[7] == -1 ? "s_8=NULL" : "s_8="+equippedItems[7])}, hp={player.HP}, armor={player.ARMOR}, str={player.STR}, vit={player.VIT}, dex={player.DEX}, luck={player.LUCK}";

        Debug.Log(queryString); 

        connector.UpdateDB(queryString);
        //connector.Insert($"INSERT INTO Testowa (Pesos) VALUES ({player.STR})");
        Debug.Log("Dodano");

    }

}
