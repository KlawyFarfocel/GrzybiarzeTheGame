using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using UnityEngine;

public class luck : MonoBehaviour
{
    private DBConnector dbConnector;
    void Start()
    {
        dbConnector = GameObject.Find("TopPanel").GetComponent<DBConnector>();

    }
    // Start is called before the first frame update
    void OnMouseDown()
    {
        IDataReader selectm = dbConnector.Select("SELECT money FROM postac");
        while (selectm.Read())
        {
            IDataReader selectd = dbConnector.Select("SELECT luck_c FROM click");

            while (selectd.Read())
            {
                Debug.Log(selectd.GetInt32(0));
                Debug.Log(selectm.GetInt32(0));
                if (selectd.GetInt32(0) <= selectm.GetInt32(0))
                {
                    int ale = selectm.GetInt32(0) - selectd.GetInt32(0);
                    string updateQuery2 = "UPDATE postac SET money = " + ale.ToString();
                    dbConnector.UpdateDB(updateQuery2);
                    int a = 0;
                    string b;
                    int a1 = 0;
                    string b1;
                    string updateQuery;
                    string updateQuery1;
                    IDataReader selectAllFromPlayer = dbConnector.Select("SELECT luck FROM postac");
                    while (selectAllFromPlayer.Read())
                    {
                        b = selectAllFromPlayer[0].ToString();
                        a = Int32.Parse(b);
                        a++;
                        updateQuery = "UPDATE postac SET luck = " + a;
                        //Debug.Log(updateQuery);
                        dbConnector.UpdateDB(updateQuery);
                    }

                    IDataReader selectAllFromPlayer1 = dbConnector.Select("SELECT luck_c FROM click");
                    while (selectAllFromPlayer1.Read())
                    {
                        b1 = selectAllFromPlayer1[0].ToString();
                        a1 = Int32.Parse(b1);
                        a1 += 10;

                        updateQuery1 = "UPDATE click SET luck_c = " + a1;
                        dbConnector.UpdateDB(updateQuery1);
                        //Debug.Log(updateQuery);


                    }

                }
            }

        }

    }
}


