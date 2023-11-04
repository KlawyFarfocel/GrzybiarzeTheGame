using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class test : MonoBehaviour
{
    public DBConnector dbConnector;
    public void Start()
    {
        dbConnector = GameObject.Find("DBHandler").GetComponent<DBConnector>();
    }
    private void Update()
    {
        if(dbConnector.dbcon!=null) 
        {
            dbConnector = GameObject.Find("DBHandler").GetComponent<DBConnector>();
        }
    }
}
