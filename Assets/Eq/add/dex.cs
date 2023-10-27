using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using UnityEngine;

public class dex : MonoBehaviour
{
    private DBConnector dbConnector;
    void Start()
    {
        dbConnector = GameObject.Find("TopPanel").GetComponent<DBConnector>();

    }
    // Start is called before the first frame update
    void OnMouseDown()
    {
        int a = 0;
        string b;
        string updateQuery;
        IDataReader selectAllFromPlayer = dbConnector.Select("SELECT dex FROM postac");
        while (selectAllFromPlayer.Read())
        {
            b = selectAllFromPlayer[0].ToString();
            a = Int32.Parse(b);
            a++;
            updateQuery = "UPDATE postac SET dex = " + a;
            //Debug.Log(updateQuery);
            dbConnector.UpdateDB(updateQuery);
        }
        //Debug.Log(updateQuery);
        // dbConnector.UpdateDB(updateQuery);
    }
}


