using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.UI;

public class eqManager : MonoBehaviour
{
    public List<int> allCollectedItemsIdList= new();
    public List<eqItem> allCollectedItems = new();
    public DBConnector dbCon;
    public List<int> equippedItemsIdList = new();
    // Start is called before the first frame update
    string generateINQuery(string query, List<int> inList)
    {
        query += " IN (";
        foreach (var item in inList)
        {
            query += $"{item}, ";
        }
        query=query.Substring(0, query.Length - 2);
        query += ")";
        return query;
    }
    void checkEquippedItems()
    {
        void checkIfNotNull(object value)
        {
            if (value != DBNull.Value)
            {
                equippedItemsIdList.Add(
                    Int32.Parse(value.ToString())
                );
            }
            else
            {
                equippedItemsIdList.Add(-1);
            }
        }
        equippedItemsIdList.Clear();
        IDataReader getEquippedItemsQuery = dbCon.Select("SELECT s_1,s_2,s_3,s_4,s_5,s_6,s_7,s_8 FROM postac");
        while (getEquippedItemsQuery.Read()) 
        {
            checkIfNotNull(getEquippedItemsQuery[0]);
            checkIfNotNull(getEquippedItemsQuery[1]);
            checkIfNotNull(getEquippedItemsQuery[2]);
            checkIfNotNull(getEquippedItemsQuery[3]);
            checkIfNotNull(getEquippedItemsQuery[4]);
            checkIfNotNull(getEquippedItemsQuery[5]);
            checkIfNotNull(getEquippedItemsQuery[6]);
            checkIfNotNull(getEquippedItemsQuery[7]);
        }
    }
    void Start()
    {
        dbCon=GameObject.Find("EqSlots").GetComponent<DBConnector>();
        IDataReader getAllCollectedItems = dbCon.Select("SELECT item_id FROM all_eq");
        while (getAllCollectedItems.Read())
        {
            allCollectedItemsIdList.Add(
                Int32.Parse(getAllCollectedItems[0].ToString())
            );//dodaj id itemów do listy
        }
        string collectedItemsDataQuery=generateINQuery("SELECT * FROM eq WHERE eq_id", allCollectedItemsIdList);
        IDataReader getAllCollectedItemsData = dbCon.Select(collectedItemsDataQuery);
        while(getAllCollectedItemsData.Read())
        {
            eqItem eqItem = new();
            eqItem.eq_id = Int32.Parse(getAllCollectedItemsData[0].ToString());
            eqItem.slot_id= Int32.Parse(getAllCollectedItemsData[1].ToString());
            eqItem.armor= Int32.Parse(getAllCollectedItemsData[2].ToString());

            eqItem.mod_1= Int32.Parse(getAllCollectedItemsData[3].ToString());
            eqItem.mod_1_val = Int32.Parse(getAllCollectedItemsData[4].ToString());

            if (getAllCollectedItemsData[5]!=DBNull.Value)
            {
                eqItem.mod_2 = Int32.Parse(getAllCollectedItemsData[5].ToString());
                eqItem.mod_2_val = Int32.Parse(getAllCollectedItemsData[6].ToString());
            }
            else
            {
                eqItem.mod_2 = -1;
                eqItem.mod_2_val = -1;
            }

            if (getAllCollectedItemsData[7] != DBNull.Value)
            {
                eqItem.mod_4 = Int32.Parse(getAllCollectedItemsData[7].ToString());
                eqItem.mod_4_val = Int32.Parse(getAllCollectedItemsData[8].ToString());
            }
            else
            {
                eqItem.mod_4 = -1;
                eqItem.mod_4_val = -1;
            }

            if (getAllCollectedItemsData[9] != DBNull.Value)
            {
                eqItem.mod_4 = Int32.Parse(getAllCollectedItemsData[9].ToString());
                eqItem.mod_4_val = Int32.Parse(getAllCollectedItemsData[10].ToString());
            }
            else
            {
                eqItem.mod_4 = -1;
                eqItem.mod_4_val = -1;
            }
            eqItem.name = getAllCollectedItemsData[11].ToString();
            eqItem.sprite= getAllCollectedItemsData[12].ToString();

            allCollectedItems.Add(eqItem);
        }
        checkEquippedItems();
        loadEquippedItems();
        putOnItem("Backpack", allCollectedItems.Find(item => item.slot_id == 2));
    }

    void loadEquippedItems()
    {
        //Ubierz Heada
        int HeadItemEquippedId = equippedItemsIdList[0];
        eqItem headItem = allCollectedItems.Find(item => item.slot_id == HeadItemEquippedId);
        putOnItem("Helm", headItem);

        int ChestItemEquippedId = equippedItemsIdList[1];
        eqItem ChestItem = allCollectedItems.Find(item => item.slot_id == ChestItemEquippedId);
        putOnItem("Chest", ChestItem);

        int BackpackItemEquippedId = equippedItemsIdList[2];
        eqItem BackpackItem = allCollectedItems.Find(item => item.slot_id == BackpackItemEquippedId);
        putOnItem("Backpack", BackpackItem);

        int HandsItemEquippedId = equippedItemsIdList[3];
        eqItem HandsItem = allCollectedItems.Find(item => item.slot_id == HandsItemEquippedId);
        putOnItem("Accessories", HandsItem);

        int LegsItemEquippedId = equippedItemsIdList[4];
        eqItem LegsItem = allCollectedItems.Find(item => item.slot_id == LegsItemEquippedId);
        putOnItem("Legs", LegsItem);

        int BootsItemEquippedId = equippedItemsIdList[5];
        eqItem BootsItem = allCollectedItems.Find(item => item.slot_id == BootsItemEquippedId);
        putOnItem("Boots", BootsItem);

        int MainHandItemEquippedId = equippedItemsIdList[6];
        eqItem MainHandItem = allCollectedItems.Find(item => item.slot_id == MainHandItemEquippedId);
        putOnItem("MainHand", MainHandItem);

        int OffHandItemEquippedId = equippedItemsIdList[7];
        eqItem OffHandItem = allCollectedItems.Find(item => item.slot_id == OffHandItemEquippedId);
        putOnItem("OffHand", OffHandItem);
    }
    void putOnItem(string ParentName,eqItem itemtoWear)
    {
        if(itemtoWear != null)
        {
            GameObject Parent = GameObject.Find($"{ParentName}");
            GameObject Child = Parent.transform.GetChild(0).gameObject;
            Child.SetActive(true);
            Child.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + itemtoWear.sprite);

            //Tutaj logika do dodawania statystyk
        }
    }
}
