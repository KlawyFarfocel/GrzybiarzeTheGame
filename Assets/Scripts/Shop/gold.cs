using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class gold : MonoBehaviour
{
    public GameObject textmeshpro_text;

    TextMeshProUGUI textmeshpro_text_text;
    private DBConnector dbConnector;
    // Start is called before the first frame update
    void Start()
    {
        dbConnector = GameObject.Find("TopPanel").GetComponent<DBConnector>();
        textmeshpro_text_text = textmeshpro_text.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        IDataReader selectAllFromPlayer = dbConnector.Select("SELECT money FROM postac");
        while (selectAllFromPlayer.Read())
        {
          //  Debug.Log(selectAllFromPlayer.ToString()) ;
            textmeshpro_text_text.text = selectAllFromPlayer[0].ToString();
           
        }
    }
}
