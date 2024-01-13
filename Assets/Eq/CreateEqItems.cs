using System;
using System.Data;
using UnityEngine;

public class CreateEqItems : MonoBehaviour
{
    public static CreateEqItems Instance;
    private GameObject CreatedItem;
    private Slot slot;
    public Transform parentTransform;
    private DBConnector dbConnector;
    public GameObject ItemPrefab;

    public Player playerData;


    private void Start()
    {
        InventoryManager inventoryManager = InventoryManager.Instance;

    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void SpawnObject(Item item)
    {

        //EqManager.Instance.AddItem(item);
    }

    public eqItem RespItem()
    {
        dbConnector = GameObject.Find("DialogueManager").GetComponent<DBConnector>();
        IDataReader GetItemsCount = dbConnector.Select("Select count(*) FROM eq");
        string Count = GetItemsCount[0].ToString();
        int ItemCount = Int32.Parse(Count);

        //pobranie z bazy itemu o losowym id
        int RandomIndex = UnityEngine.Random.Range(0, ItemCount);

        eqItem item = new eqItem();
        IDataReader createItem = dbConnector.Select($"SELECT * FROM eq WHERE eq_id = {RandomIndex}");
        int itemSlot_id=0;
        int item_id=0;
        while (createItem.Read())
        {
            string id = createItem[0].ToString();
            int ID = Int32.Parse(id);
            Debug.Log($"{id} oraz {ID}");

            string s_id = createItem[1].ToString();
            int S_ID = Int32.Parse(s_id);
            string armor = createItem[2].ToString();
            int ARMOR = Int32.Parse(armor);

            string mod_1 = createItem[3].ToString();
            int? MOD_1 = string.IsNullOrEmpty(mod_1) ? (int?)null : Int32.Parse(mod_1);

            string mod_1_val = createItem[4].ToString();
            int? MOD_1_VAL = string.IsNullOrEmpty(mod_1_val) ? (int?)null : Int32.Parse(mod_1_val);

            string mod_2 = createItem[5].ToString();
            int? MOD_2 = string.IsNullOrEmpty(mod_2) ? (int?)null : Int32.Parse(mod_2);

            string mod_2_val = createItem[6].ToString();
            int? MOD_2_VAL = string.IsNullOrEmpty(mod_2_val) ? (int?)null : Int32.Parse(mod_2_val);

            string mod_3 = createItem[7].ToString();
            int? MOD_3 = string.IsNullOrEmpty(mod_3) ? (int?)null : Int32.Parse(mod_3);

            string mod_3_val = createItem[8].ToString();
            int? MOD_3_VAL = string.IsNullOrEmpty(mod_3_val) ? (int?)null : Int32.Parse(mod_3_val);

            string mod_4 = createItem[9].ToString();
            int? MOD_4 = string.IsNullOrEmpty(mod_4) ? (int?)null : Int32.Parse(mod_4);

            string mod_4_val = createItem[10].ToString();
            int? MOD_4_VAL = string.IsNullOrEmpty(mod_4_val) ? (int?)null : Int32.Parse(mod_4_val);

            string NAME = createItem[11].ToString();
            string SPRITE = createItem[12].ToString();

            //zaczytanie prefabu i stworzenie itemu na ekranie
            /*
            GameObject ItemPrefab = Resources.Load<GameObject>("Prefabs/Item");
            GameObject CreatedItem = Instantiate(ItemPrefab);
            CreatedItem.transform.SetParent(GameObject.Find("Las").transform, true);
            item = CreatedItem.GetComponent<eqItem>();
            */

            item.eq_id = ID;
            item.slot_id = S_ID;
            item.armor = ARMOR;
            item.mod_1 = MOD_1 ?? 0;
            item.mod_1_val = MOD_1_VAL ?? 0;
            item.mod_2 = MOD_2 ?? 0;
            item.mod_2_val = MOD_2_VAL ?? 0;
            item.mod_3 = MOD_3 ?? 0;
            item.mod_3_val = MOD_3_VAL ?? 0;
            item.mod_4 = MOD_4 ?? 0;
            item.mod_4_val = MOD_4_VAL ?? 0;
            item.name = NAME;
            item.sprite = SPRITE;

            /*
            Sprite itemsprite = Resources.Load<Sprite>($"Items/{SPRITE}");
            CreatedItem.GetComponent<SpriteRenderer>().sprite = itemsprite;
            CreatedItem.GetComponent<SpriteRenderer>().sortingOrder = -2;
            */

            itemSlot_id = S_ID;
            item_id = S_ID;

            //narazie wy³¹czone ¿eby nie dodawa³o do bazy
            
        }
       dbConnector.Insert($"Insert INTO all_eq(item_id,slot_id) VALUES({item_id} , {itemSlot_id})");
        return (item);
    }

    public void GenerateChest()
    {
        GameObject ChestPrefab = Resources.Load<GameObject>("Prefabs/Chest");
        GameObject Chest = Instantiate(ChestPrefab);
        Chest.transform.SetParent(GameObject.Find("Las").transform, true);
        Chest.GetComponent<RectTransform>().localPosition = new Vector3(-45, 400, 1);
        Chest.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}