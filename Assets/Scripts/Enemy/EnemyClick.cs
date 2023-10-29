using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyClick : MonoBehaviour
{
    private Player player;
    private InventoryManager items = InventoryManager.Instance;
    private BackgroundManager bgManager;
    private LevelManager levelManager;


    public void EnemyClickAction(GameObject enemy)
    {
        player = GameObject.Find("emerytka").GetComponent<Player>();
        bgManager = GameObject.Find("Background").GetComponent<BackgroundManager>();

        Enemy enemyData = enemy.GetComponent<Enemy>();
        Player playerdata = player.GetComponent<Player>();

        //testowa wersja walki po kliknieciu odebranie hp 
     
            enemyData.HP -= playerdata.STR;
            playerdata.HP -= 1;
            Debug.Log("enemy hp " + enemyData.HP);
            Debug.Log("player hp " + playerdata.HP);
        

        if (playerdata.HP <= 0)
        {
            Debug.Log("YOU DIED");

            //usuwanie grzybow 
            foreach (Slot slot in items.slots)
            {
                slot.RemoveItemFromSlot();
            }
            //ustawienie etapu na 1 
            levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            levelManager.level = 1;
            levelManager.currentClicks = 1;
            bgManager.changeValues(levelManager.level);
            Destroy(enemy);
        }

        if (enemyData.HP <= 0)
        {
            Debug.Log("Enemy died");
            Destroy(enemy);
            //pewnie resp skyrznki po wygranej 

        }
    }
}
