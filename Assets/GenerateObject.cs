using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GenerateObject : MonoBehaviour
{
    public GameObject[] Mushrooms;
    private Vector2 position;
    private float SpawnChance = 0.15f;
    private float MinX, MaxX, MinY, MaxY;
    private int clickCount = 0;
    private GameObject activeMushroom;


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

    private void SpawnObject()
    {

        int RandomIndex = Random.Range(0, Mushrooms.Length); // losowy indeks grzyba
        position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        activeMushroom = Instantiate(Mushrooms[RandomIndex], position, Quaternion.identity);
        activeMushroom.name = Mushrooms[RandomIndex].name;

    }

    private void TrySpawnObject()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue < SpawnChance)
        {
            SpawnObject();
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
                Debug.Log("Score: " + 1);
                Destroy(activeMushroom);
                TrySpawnObject();
            }
            else if (activeMushroom != null && clickCount >= 2)
            {
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
