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

    private void Start()
    {
        dbConnector = GameObject.Find("Las").GetComponent<DBConnector>();
        TrySpawnEnemy(2);
    }

    public void TrySpawnEnemy(int enemyID)
    {
        IDataReader SelectEnemy = dbConnector.Select($"SELECT * FROM enemy WHERE Id = {enemyID} ");
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

            GameObject enemy = Instantiate(EnemyPrefab);
            Enemy enemyScript = enemy.GetComponent<Enemy>();

            enemyScript.HP = HP;
            enemyScript.ID = ID;
            enemyScript.Name = Name;
            enemyScript.Damage = DMG;

            //nazwa sprite taka jak zwykla nazwa
            Sprite enemySprite = Resources.Load<Sprite>(Name);
            enemy.GetComponent<SpriteRenderer>().sprite = enemySprite;
        }
    }
}
