using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
using static UnityEngine.EventSystems.EventTrigger;

public class BackgroundManager : MonoBehaviour
{
    public DBConnector dbCon;
    public GameObject background;
    public LevelManager levelManager;
    // Start is called before the first frame update

    void Start()
    {
        dbCon = GameObject.Find("Main Camera").GetComponent<DBConnector>();
        background = GameObject.Find("Background");
        levelManager=GameObject.Find("LevelManager").GetComponent<LevelManager>();

        int level = levelManager.level;
        changeValues(level);
    }
    public void changeValues(int level)
    {
        IDataReader SelectBG = dbCon.Select($"SELECT * FROM Background WHERE bg_id = {level}");
        while (SelectBG.Read())
        {
            string sprite = SelectBG[1].ToString();
            float posX = float.Parse(SelectBG[2].ToString());
            float posY = float.Parse(SelectBG[3].ToString());
            float scaleX = float.Parse(SelectBG[4].ToString());
            float scaleY = float.Parse(SelectBG[5].ToString());

            Sprite BGSprite = Resources.Load<Sprite>(sprite);
            background.GetComponent<SpriteRenderer>().sprite = BGSprite;

            Vector3 newPosition = new Vector3(posX, posY, -250);
            Vector3 newScale = new Vector3(scaleX, scaleY, -250);
            background.GetComponent<Transform>().localPosition = newPosition;
            background.GetComponent<Transform>().localScale = newScale;
        }
        IDataReader SelectClicks = dbCon.Select($"SELECT click_count FROM spot WHERE spot_id = {level}");
        while (SelectClicks.Read())
        {
            levelManager.clicksTarget = Int32.Parse(SelectClicks[0].ToString());
        }
        GameObject.Find("LevelProgress").GetComponent<TextMeshProUGUI>().text = $"Poziom: {levelManager.level} <br> Etap: {levelManager.currentClicks}/{levelManager.clicksTarget}";
    }
}
