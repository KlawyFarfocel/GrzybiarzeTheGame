using System;
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

    private bool isRolling = false;

    private int rollResult;
    private string rollEffect;

    public void HandleRollAnimation()
    {
        GameObject RollPrefab = Resources.Load<GameObject>("Prefabs/RollHolder");
        GameObject RollHolder = Instantiate(RollPrefab);
        GameObject D20 = RollHolder.transform.GetChild(0).GetChild(0).gameObject;

        isRolling = true;
        System.Random random = new System.Random();
         rollResult = random.Next(0, 21);
        Animator rollAnim = GameObject.Find("d20").GetComponent<Animator>();
        rollAnim.SetBool("isRunning", true);
        //============TUTAJ DAJ WYNIK Z LOSOWANIA============
       // rollResult = 20; // tutaj ile wypadlo
        Variables.Object(D20).Set("result", rollResult);
        //============TUTAJ DAJ CZY SIE UDA£O CZY NIE============
        
        if(rollResult == 1)
            rollEffect = "FAIL";
        else
            rollEffect = "SUCCESS"; //FAIL albo SUCCESS
        Variables.Object(D20).Set("effect", rollEffect);

        
        rollAnim.SetBool("isRunning", false);

        RollHolder.transform.SetParent(GameObject.Find("Las").transform, true);
        RollHolder.transform.localPosition = new Vector3(-250, 350, 0);
        RollHolder.transform.localScale = new Vector3(1, 1, 1);

        Destroy(GameObject.Find("RollHolder(Clone)"),2f);
        StartCoroutine(WaitForRoll(2));

    }
    IEnumerator WaitForRoll(int secs) // tutaj w³¹cza mozliwoœæ klikania dopiero po 2 sekundach od wylosowania
    {
        yield return new WaitForSeconds(secs);
        isRolling = false;
    }
    public void HandleDMGCalc(GameObject enemy) //tutaj ogarnianie walki
    {
        player = GameObject.Find("emerytka").GetComponent<Player>();
        bgManager = GameObject.Find("Background").GetComponent<BackgroundManager>();

        Enemy enemyData = enemy.GetComponent<Enemy>();
        Player playerdata = player.GetComponent<Player>();

        //testowa wersja walki po kliknieciu odebranie hp 

       

        if (rollEffect == "FAIL") //miss - nie trafiles i chuj //enemy cie uderza tak czy siak
        {
            playerdata.HandleHealthLoss(1050); // to odejnuje graczowi hp, do obarniecia
        }
        else
        {
            if (rollResult == 20)//kryt
            {
                enemyData.HP -= (int)(playerdata.STR * (rollResult+2)/10); // do zmiany pewnie
            }
            else
            { //normalny hit
                enemyData.HP -= (int)(playerdata.STR * rollResult / 10);
            }
            playerdata.HandleHealthLoss(1050); // to odejnuje graczowi hp, do obarniecia
        }
        
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
            CreateItem.GenerateChest();
        }
    }
    public void EnemyClickAction(GameObject enemy)
    {
        if (!isRolling) //jak sie rolluje nie da sie klikaæ na enemy
        {
            HandleRollAnimation();
        }
    }
}
