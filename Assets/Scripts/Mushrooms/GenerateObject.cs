using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using System;
using Random = UnityEngine.Random;
using UnityEngine.U2D;
using static UnityEngine.EventSystems.EventTrigger;

public class GenerateObject : MonoBehaviour
{
    public List<Item> mushroomsList;
    private Vector2 position;
    private float MinX, MaxX, MinY, MaxY;
    private int clickCount = 0;
    private GameObject activeMushroom;
    private bool isMushroomSpawned = false;
    public GameObject prefab;
    public DBConnector connector;
    public SpawnEnemy spawnEnemy;

    void Start()
    {
        SetMinMax();
        connector = GameObject.Find("Las").GetComponent<DBConnector>();
        GetMushrooms();
    }
    private void SetMinMax()
    {
        Vector2 Bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        MinX= -Bounds.x / 2;
        MaxX= Bounds.x /2;
        MinY= -Bounds.y / 2;
        MaxY= Bounds.y / 2;
    }
    private void GetMushrooms()
    {
        IDataReader GetMushrooms = connector.Select("SELECT * FROM Item");
        while (GetMushrooms.Read())
        {
            GameObject itemGO = new GameObject("Item");
            Item mushroom = itemGO.AddComponent<Item>();

            string id = GetMushrooms[0].ToString();
            int ID = Int32.Parse(id);
            string Name = GetMushrooms[1].ToString();
            string value = GetMushrooms[2].ToString();
            int VALUE = Int32.Parse(value);
            string SPRITE = GetMushrooms[3].ToString();
            string resp = GetMushrooms[4].ToString();
            float RespChance = float.Parse(resp);


            mushroom.ID = ID;
            mushroom.Name = Name;
            mushroom.Value = VALUE;
            mushroom.Sprite = Resources.Load<Sprite>(SPRITE);
            mushroom.sprite = SPRITE;
            mushroom.RespChange = RespChance;
            mushroomsList.Add(mushroom);
        }
    }


    private void SpawnObject(Item item)
    {
        float averageScale = 0.8f;
        position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        Collider2D overlap = Physics2D.OverlapCircle(position, averageScale);
        if (overlap == null)
        {
            activeMushroom = Instantiate(prefab, position, Quaternion.identity);
            Item itemscript = activeMushroom.GetComponent<Item>();
            Sprite shroomSprite = Resources.Load<Sprite>(item.sprite);

            itemscript.ID = item.ID;
            itemscript.Name = item.Name;
            itemscript.Sprite = shroomSprite;
            itemscript.Value = item.Value;
            itemscript.RespChange = item.RespChange;

            activeMushroom.GetComponent<SpriteRenderer>().sprite = shroomSprite;
            activeMushroom.GetComponent<ItemObject>().item = item;
        }
        else
        {
            SpawnObject(item);
        }
         
    }

    private void TrySpawnObject()
    {
        if (!isMushroomSpawned)
        {
            float randomValue = Random.Range(0f, 1f);
            float AllRespChance = 0f;

            foreach (Item shroom in mushroomsList)
            {
                AllRespChance += shroom.RespChange / 100f;
                if (randomValue < AllRespChance)
                {
                    int RandomIndex = Random.Range(0, mushroomsList.Count); // losowy indeks grzyba
                    SpawnObject(mushroomsList[RandomIndex]);
                    isMushroomSpawned = true;
                    break;
                }
            }
        }
    }

    private void TrySpawnMob()
    {
        spawnEnemy = GameObject.Find("EnemySpawner").GetComponent<SpawnEnemy>();
        float randomValue = Random.Range(0f, 1f);
        //2 moby szansa ustawiana tutaj 1%
        float respchange = 0.15f;
        if(randomValue < respchange)
        {
            int index = Random.Range(0, 3);
            spawnEnemy.TrySpawnEnemy(index);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (activeMushroom != null)
            {
                clickCount++;
            }
                
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == activeMushroom)
            {
                activeMushroom.SetActive(false);  
                isMushroomSpawned = false;
                Destroy(activeMushroom);
                TrySpawnObject();

            }
            else if (activeMushroom != null && clickCount >= 2)
            {

                activeMushroom.SetActive(false);
                isMushroomSpawned = false;
                Destroy(activeMushroom);
                clickCount = 0;
            }
            else
            {
                TrySpawnObject();
                TrySpawnMob();
            }
        }
    }
}
