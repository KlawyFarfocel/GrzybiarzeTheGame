using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnEnemy : MonoBehaviour
{
    private DBConnector dbConnector;
    public GameObject EnemyPrefab;
    public GameObject Player;

    private static GameObject spawnedEnemy;

    private void Start()
    {
        dbConnector = GameObject.Find("Las").GetComponent<DBConnector>();
        if (spawnedEnemy != null) { spawnedEnemy.SetActive(true); };
    }

    public void TrySpawnEnemy(int enemyID)
    {
        dbConnector = GameObject.Find("DialogueManager").GetComponent<DBConnector>();

        IDataReader SelectEnemy = dbConnector.Select($"SELECT * FROM enemy WHERE Id = {enemyID}");
        while (SelectEnemy.Read())
        {
            string id = SelectEnemy[0].ToString();
            int ID = Int32.Parse(id);
            string hp = SelectEnemy[1].ToString();
            int HP = Int32.Parse(hp);
            string Name = SelectEnemy[2].ToString();
            string dmg = SelectEnemy[3].ToString();
            int DMG = Int32.Parse(dmg);
            //string SpriteName = SelectEnemy[4].ToString();

            if (spawnedEnemy == null)
            {
                //resp przeciwnika
                GameObject enemy = Instantiate(EnemyPrefab);
                spawnedEnemy = enemy;

                //nie usuwaj przeciwnika
                DontDestroyOnLoad(spawnedEnemy);

                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.HP = HP;
                enemyScript.ID = ID;
                enemyScript.Name = Name;
                enemyScript.Damage = DMG;

                Sprite enemySprite = Resources.Load<Sprite>(Name);
                enemy.GetComponent<SpriteRenderer>().sprite = enemySprite;
            }
            else
            {
                //jesli juz istnieje to update statow
                Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
                enemyScript.HP = HP;
                enemyScript.ID = ID;
                enemyScript.Name = Name;
                enemyScript.Damage = DMG;
            }
        }
    }

    public void OnDestroy()
    {
        if (spawnedEnemy != null) { spawnedEnemy.SetActive(false); };
    }
}
