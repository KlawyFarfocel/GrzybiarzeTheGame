using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GenerateObject : MonoBehaviour
{
    public Item[] Muhsrooms;
    //public GameObject[] Mushrooms;
    private Vector2 position;
    private float MinX, MaxX, MinY, MaxY;
    private int clickCount = 0;
    private GameObject activeMushroom;
    private bool isMushroomSpawned = false;

    void Start()
    {
        SetMinMax();
    }
    private void SetMinMax()
    {
        Vector2 Bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        MinX= -Bounds.x / 2;
        MaxX= Bounds.x /2;
        MinY= -Bounds.y / 2;
        MaxY= Bounds.y / 2;
    }

    private void SpawnObject(Item item)
    {

        position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        // activeMushroom = item.prefab;
        //  activeMushroomItem = item;
        // activeMushroom.name = item.name;
        activeMushroom = Instantiate(item.prefab, position, Quaternion.identity);
        activeMushroom.AddComponent<ItemObject>().item = item;
        //  Instantiate(activeMushroom, position, Quaternion.identity);

    }

    private void TrySpawnObject()
    {

        if (!isMushroomSpawned)
        {
            float randomValue = Random.Range(0f, 1f);
            float AllRespChance = 0f;

            foreach (Item item in Muhsrooms)
            {
                AllRespChance += item.RespChange / 100f;
                if (randomValue < AllRespChance)
                {
                    int RandomIndex = Random.Range(0, Muhsrooms.Length); // losowy indeks grzyba
                    SpawnObject(Muhsrooms[RandomIndex]);
                    isMushroomSpawned = true;
                    break;
                }
            }
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
                //activeMushroom.SetActive(false);  
                isMushroomSpawned = false;
                Destroy(activeMushroom);
                TrySpawnObject();

            }
            else if (activeMushroom != null && clickCount >= 2)
            {

                //activeMushroom.SetActive(false);
                isMushroomSpawned = false;
                 Destroy(activeMushroom);
                clickCount = 0;
            }
            else
            {
               TrySpawnObject();
            }
        }
    }
}
