using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyClick : MonoBehaviour
{
    private Player player;
    private InventoryManager items = InventoryManager.Instance;
    private BackgroundManager bgManager;
    private LevelManager levelManager;
    private CreateEqItems CreateItem;
    public bool isRolling = false;

    private DBConnector dbConnector;
    public TextEffect textEffect;

    private int rollResult;
    private string rollEffect;

    public void HandleRollAnimation()
    {
        GameObject RollPrefab = Resources.Load<GameObject>("Prefabs/RollHolder");
        GameObject RollHolder = Instantiate(RollPrefab);
        GameObject D20 = RollHolder.transform.GetChild(0).GetChild(0).gameObject;

        isRolling = true;
        System.Random random = new System.Random();
         rollResult = random.Next(1, 21);
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

    private IEnumerator changeSceneAfterTime(GameObject enemy)
    {
        yield return new WaitForSeconds(0.45f);

        //usuwanie grzybow 
        items.Clear();
        //ustawienie etapu na 1 
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.level = 1;
        levelManager.currentClicks = 0;
        player.CURRENT_HP = player.HP;
        player.HandleUIChange();
        bgManager.changeValues(levelManager.level);
        Destroy(enemy);
    }

    public void HandleDMGCalc(GameObject enemy) //tutaj ogarnianie walki
    {
        player = GameObject.Find("LevelManager").GetComponent<Player>();
        bgManager = GameObject.Find("Background").GetComponent<BackgroundManager>();

        Enemy enemyData = enemy.GetComponent<Enemy>();
        Player playerdata = player.GetComponent<Player>();

        //testowa wersja walki po kliknieciu odebranie hp 
        double zmienna = 0;
       if(playerdata.ARMOR<1500)
        {
            zmienna = 0.90;
        }
        if (playerdata.ARMOR < 3500 && playerdata.ARMOR>1500)
        {
            zmienna = 0.80;
        }
        if (playerdata.ARMOR < 5000 && playerdata.ARMOR > 3500)
        {
            zmienna = 0.70;
        }
        if (playerdata.ARMOR < 8000 && playerdata.ARMOR > 5000)
        {
            zmienna = 0.60;
        }
        if (playerdata.ARMOR >8000)
        {
            zmienna = 0.40;
        }
        if (rollEffect == "FAIL") //miss - nie trafiles i chuj //enemy cie uderza tak czy siak
        {
            playerdata.HandleHealthLoss(enemyData.Damage); // to odejnuje graczowi hp, do obarniecia
            Debug.Log(enemyData.HP -= (int)(playerdata.STR * (rollResult + 2) / 10));
        }
        else
        {
            if (rollResult == 20)//kryt
            {
                enemyData.HP -= (int)(playerdata.STR * (rollResult+2)/10); // do zmiany pewnie //wyjebac 150 bo bylo do testowania
                Debug.Log(enemyData.HP -= (int)(playerdata.STR * (rollResult + 2) / 10));
            }
            else
            { //normalny hit
                enemyData.HP -= (int)(playerdata.STR * rollResult / 10); //wyjebac 150 bo bylo do testowania
                Debug.Log(enemyData.HP -= (int)(playerdata.STR * (rollResult) / 10));
            }
            if(enemyData.HP > 1)
            playerdata.HandleHealthLoss((int)((double)enemyData.Damage*zmienna)); // to odejnuje graczowi hp, do obarniecia
        }
        // 8k to 50 % redukcji dmg i to jest cap
        // 5k to 30 % redukcji i to jest soft cap
        // 3.5-5k to 20 % redukcji
        // 1.5-3.5k to 15% redukcji
        // <1.5k to 5% redukcji
        Debug.Log("enemy hp " + enemyData.HP);
        Debug.Log("enemy str " + playerdata.ARMOR);
        Debug.Log("player hp " + playerdata.HP);
        Debug.Log(zmienna);
        int gold = enemyData.Damage*2; // gold z moba



        if (playerdata.CURRENT_HP <= 0)
        {
            int level = GameObject.Find("LevelManager").GetComponent<LevelManager>().level;
            if (level == 5)
            {
                DialogueManager dManager=GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
                dManager.ToggleDialoguePanel(true);
                dManager.FindDialogue("end");
                return;
            }
            GameObject.Find("DeathScreen").GetComponent<Animation>().Play();

            IEnumerator changeSceneCoroutine = changeSceneAfterTime(enemy);
            StartCoroutine(changeSceneCoroutine);
        }

        if (enemyData.HP <= 0)
        {
            CreateItem = new CreateEqItems();
            Debug.Log("Enemy died");
            Destroy(enemy);
            if(enemyData.ID > 2)
            {
                CreateItem.GenerateChest();
            }
            else
            {
                //gold po pokananiu moba
                dbConnector = GameObject.Find("DialogueManager").GetComponent<DBConnector>();
                string updateQuery = $"UPDATE postac SET money = money+{gold}";
                dbConnector.UpdateDB(updateQuery);
                //zagraj animacje coina
                GameObject.Find("goldCoin").GetComponent<Animation>().Play();
            }

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
