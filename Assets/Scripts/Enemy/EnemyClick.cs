using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyClick : MonoBehaviour
{
    private Player player;
    private InventoryManager items = InventoryManager.Instance;
    private BackgroundManager bgManager;
    private LevelManager levelManager;
    private CreateEqItems CreateItem;

    public void HandleRollAnimation()
    {
        GameObject RollPrefab = Resources.Load<GameObject>("Prefabs/RollHolder");
        GameObject RollHolder = Instantiate(RollPrefab);
        GameObject D20 = RollHolder.transform.GetChild(0).GetChild(0).gameObject;
        //============TUTAJ DAJ WYNIK Z LOSOWANIA============
        Variables.Object(D20).Set("result", 20);
        //============TUTAJ DAJ CZY SIE UDA£O CZY NIE============
        Variables.Object(D20).Set("effect", "FAIL");

        RollHolder.transform.SetParent(GameObject.Find("Las").transform, true);
        RollHolder.transform.localPosition = new Vector3(-250, 350, 0);
        RollHolder.transform.localScale = new Vector3(1, 1, 1);
    }
    public void EnemyClickAction(GameObject enemy)
    {
        player = GameObject.Find("emerytka").GetComponent<Player>();
        bgManager = GameObject.Find("Background").GetComponent<BackgroundManager>();

        Enemy enemyData = enemy.GetComponent<Enemy>();
        Player playerdata = player.GetComponent<Player>();

        //testowa wersja walki po kliknieciu odebranie hp 
     
            enemyData.HP -= playerdata.STR;
            HandleRollAnimation();
            playerdata.HandleHealthLoss(1050);
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
            levelManager.currentClicks = 0;
            bgManager.changeValues(levelManager.level);
            Destroy(enemy);
        }

        if (enemyData.HP <= 0)
        {
            CreateItem = new CreateEqItems();
            Debug.Log("Enemy died");
            Destroy(enemy);
            CreateItem.CreateItem();
        }
    }
}
