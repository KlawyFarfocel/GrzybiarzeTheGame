using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class eqManager : MonoBehaviour
{
    public Player playerData;

    public List<int> allCollectedItemsIdList= new();
    public List<eqItem> allCollectedItems = new();
    public DBConnector dbCon;
    public List<int> equippedItemsIdList = new();
    public List<string> allItemsNameList = new();
    public GameObject itemsMenuPopup;
    public GameObject itemStatsPanel;
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
    public void HandleEqSlotClick(GameObject slot)
    {
        if (slot.GetComponent<Image>().sprite == null) 
        {// Nie ma zalozonego itema - otworz menu i pozwol zalozyc
            string item_slot_id = Variables.Object(slot).Get("slot_id").ToString();
            
            List<int> allCollectedItemsIdOfType = new(); // id wszystkich itemów z tego slota
            List<eqItem> allColledtedItemsOfType=new();

            IDataReader getAllItemsIdOfSlot = dbCon.Select($"SELECT * FROM all_eq WHERE slot_id={item_slot_id}");
            while (getAllItemsIdOfSlot.Read())
            {
                allCollectedItemsIdOfType.Add(int.Parse(getAllItemsIdOfSlot[1].ToString()));
            }
            itemsMenuPopup.SetActive(true);
            foreach (int item in allCollectedItemsIdOfType)//Tworzenie slotów
            {
                eqItem eqItem = allCollectedItems.Find(collectedItem => collectedItem.eq_id == item);
                GameObject slotPrefab = Resources.Load<GameObject>("Prefabs/Slot");

                GameObject createdSlot = Instantiate(slotPrefab);

                Variables.Object(createdSlot).Set("item_id", eqItem.eq_id);

                createdSlot.transform.SetParent(GameObject.Find("Row").transform, true);
                createdSlot.transform.localScale = new Vector3(1, 1, 1);

                createdSlot.transform.Find("ItemIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/"+eqItem.sprite);
                createdSlot.transform.Find("ItemIcon").gameObject.SetActive(true);
                
                UnityEvent clickEvent=new UnityEvent();
                clickEvent.AddListener(() => HandleMenuItemClick(createdSlot,slot));

                EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerClick
                };
                pointerClickEntry.callback.AddListener((eventData) => clickEvent.Invoke());
                createdSlot.GetComponent<EventTrigger>().triggers.Add(pointerClickEntry);
            }
            //Klikniêcie na item w menu obs³uguje HandleMenuItemClick()
            
        }
        else  // jak ma sprite -> najpierw zdejmij item
        {// zdejmij item
            eqItem itemToRemove = allCollectedItems.FirstOrDefault(item => item.sprite == slot.GetComponent<Image>().sprite.name); //
            /*int unequippedItemId= equippedItemsIdList.Find(id => id == itemToRemove.slot_id);
            Debug.Log(unequippedItemId);*/

            //odejmowanie statow
            playerData.RemoveStats(itemToRemove);
            GameObject.Find("Main").GetComponent<Load>().EvaluateHPPoints();
            GameObject.Find("Main").GetComponent<Load>().ForceUpdateStatTexts();
            equippedItemsIdList[itemToRemove.slot_id - 1] = -1; // -1 oznacza ze nie ma zalozonego itema
            slot.GetComponent<Image>().sprite = null;
            DeleteObsoleteSlots();
        }
    }
    void DeleteObsoleteSlots()
    {
        GameObject row = GameObject.Find("Row");
        if (row != null)
        {
            int childs = row.transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.Destroy(row.transform.GetChild(i).gameObject);
            }
            itemsMenuPopup.SetActive(false);
        }

    }
    private void handleItemWear(int item_id,int slot_id)
    {
        equippedItemsIdList[slot_id - 1] = item_id;
    }
    public void HandleMenuItemClick(GameObject slot, GameObject itemFrame)//klikanie juz na item w menu zeby ubraæ
    {
        int item_id = int.Parse(Variables.Object(slot).Get("item_id").ToString());
        
        eqItem item=allCollectedItems.Find(item => item.eq_id== item_id);
        int slot_id = item.slot_id;
        itemFrame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + item.sprite);


        //otworz menu ze statystykami
        itemStatsPanel.SetActive(true);
        GameObject.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + item.sprite);
        GameObject.Find("ItemTitle").GetComponent<TextMeshProUGUI>().text = item.name;
        GameObject.Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = item.desc;

        GameObject.Find("WIT-Value").GetComponent<TextMeshProUGUI>().text = "+" + item.mod_1_val;
        GameObject.Find("DEX-Value").GetComponent<TextMeshProUGUI>().text = "+" + item.mod_2_val;
        GameObject.Find("STR-Value").GetComponent<TextMeshProUGUI>().text = "+" + item.mod_3_val;
        GameObject.Find("LUCK-Value").GetComponent<TextMeshProUGUI>().text = "+" + item.mod_4_val;
        GameObject.Find("ARM-Value").GetComponent<TextMeshProUGUI>().text = "+" + item.armor;


        //Jak klikniesz w zaloz to normalna funkcja

            UnityEvent clickEvent = new UnityEvent();
            
            clickEvent.AddListener(() =>
            {
                equippedItemsIdList[slot_id - 1] = item_id;
                itemFrame.SetActive(true);
                playerData.AddStats(item);
                GameObject.Find("Main").GetComponent<Load>().EvaluateHPPoints();
                GameObject.Find("Main").GetComponent<Load>().ForceUpdateStatTexts();
                itemStatsPanel.SetActive(false);
                DeleteObsoleteSlots(); //usuwanie itemow
                clickEvent.RemoveAllListeners();


            });
            EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            pointerClickEntry.callback.AddListener((eventData) => clickEvent.Invoke());
            GameObject.Find("WearButton").GetComponent<EventTrigger>().triggers.Add(pointerClickEntry);

            UnityEvent cancelEvent = new UnityEvent();
            cancelEvent.RemoveAllListeners();
            cancelEvent.AddListener(() =>
            {
                itemStatsPanel.SetActive(false);
            });
            EventTrigger.Entry pointerClickEntry2 = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };

            pointerClickEntry2.callback.AddListener((eventData) => cancelEvent.Invoke());
            GameObject.Find("CancelButton").GetComponent<EventTrigger>().triggers.Add(pointerClickEntry2);
    }

    void Start()
    {
        playerData = GameObject.Find("LevelManager").GetComponent<Player>();

        dbCon = GameObject.Find("EqSlots").GetComponent<DBConnector>();
        IDataReader getAllItemsNames = dbCon.Select("SELECT name FROM eq");
        while (getAllItemsNames.Read())
        {
            allItemsNameList.Add(
                getAllItemsNames[0].ToString()
            );
        }
        IDataReader getAllCollectedItems =dbCon.Select("SELECT item_id FROM all_eq");
        while (getAllCollectedItems.Read())
        {
            allCollectedItemsIdList.Add(
                Int32.Parse(getAllCollectedItems[0].ToString())
            );//dodaj id itemów do listy
        }
        string collectedItemsDataQuery = generateINQuery("SELECT * FROM eq WHERE eq_id", allCollectedItemsIdList);
        IDataReader getAllCollectedItemsData = dbCon.Select(collectedItemsDataQuery);
        while (getAllCollectedItemsData.Read())
        {
            eqItem eqItem = gameObject.AddComponent<eqItem>();
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
                eqItem.mod_2 = 0;
                eqItem.mod_2_val = 0;
            }

            if (getAllCollectedItemsData[7] != DBNull.Value)
            {
                eqItem.mod_3 = Int32.Parse(getAllCollectedItemsData[7].ToString());
                eqItem.mod_3_val = Int32.Parse(getAllCollectedItemsData[8].ToString());
            }
            else
            {
                eqItem.mod_3 = 0;
                eqItem.mod_3_val = 0;
            }

            if (getAllCollectedItemsData[9] != DBNull.Value)
            {
                eqItem.mod_4 = Int32.Parse(getAllCollectedItemsData[9].ToString());
                eqItem.mod_4_val = Int32.Parse(getAllCollectedItemsData[10].ToString());
            }
            else
            {
                eqItem.mod_4 = 0;
                eqItem.mod_4_val = 0;
            }
            eqItem.name = getAllCollectedItemsData[11].ToString();
            eqItem.sprite= getAllCollectedItemsData[12].ToString();

            if (getAllCollectedItemsData[13] != DBNull.Value)
            {
                eqItem.desc = getAllCollectedItemsData[13].ToString();
            }
            else
            {
                eqItem.desc = "";
            }


            allCollectedItems.Add(eqItem);
        }
        checkEquippedItems();
        loadEquippedItems();
    }

    void loadEquippedItems()
    {
        //Ubierz Heada
        int HeadItemEquippedId = equippedItemsIdList[0];
        eqItem headItem = allCollectedItems.Find(item => item.eq_id == HeadItemEquippedId);
        putOnItem("Helm", headItem);

        int ChestItemEquippedId = equippedItemsIdList[1];
        eqItem ChestItem = allCollectedItems.Find(item => item.eq_id == ChestItemEquippedId);
        putOnItem("Chest", ChestItem);

        int BackpackItemEquippedId = equippedItemsIdList[2];
        eqItem BackpackItem = allCollectedItems.Find(item => item.eq_id == BackpackItemEquippedId);
        putOnItem("Backpack", BackpackItem);

        int HandsItemEquippedId = equippedItemsIdList[3];
        eqItem HandsItem = allCollectedItems.Find(item => item.eq_id == HandsItemEquippedId);
        putOnItem("Accessories", HandsItem);

        int LegsItemEquippedId = equippedItemsIdList[4];
        eqItem LegsItem = allCollectedItems.Find(item => item.eq_id == LegsItemEquippedId);
        putOnItem("Legs", LegsItem);

        int BootsItemEquippedId = equippedItemsIdList[5];
        eqItem BootsItem = allCollectedItems.Find(item => item.eq_id == BootsItemEquippedId);
        putOnItem("Boots", BootsItem);

        int MainHandItemEquippedId = equippedItemsIdList[6];
        eqItem MainHandItem = allCollectedItems.Find(item => item.eq_id == MainHandItemEquippedId);
        putOnItem("MainHand", MainHandItem);

        int OffHandItemEquippedId = equippedItemsIdList[7];
        eqItem OffHandItem = allCollectedItems.Find(item => item.eq_id == OffHandItemEquippedId);
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
/*
            playerData = GameObject.Find("LevelManager").GetComponent<Player>();
            playerData.UpdateStats(itemtoWear, true , equippedItemsIdList);*/
        }
    }
}
