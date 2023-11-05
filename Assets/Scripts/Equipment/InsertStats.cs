using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class InsertStats : MonoBehaviour
{
    public DBConnector connector;
    public Player player;
    private void Start()
    {
        connector = GameObject.Find("LevelManager").GetComponent<DBConnector>();
        player = GameObject.Find("LevelManager").GetComponent<Player>();

    }
    void OnDestroy()
    {
        //connector.Insert($"INSERT INTO Testowa (Pesos) VALUES ({player.STR})");
        Debug.Log("Dodano");

    }

}
