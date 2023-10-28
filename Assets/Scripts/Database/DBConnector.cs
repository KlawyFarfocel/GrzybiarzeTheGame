using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DBConnector : MonoBehaviour
{
    // Use this for initialization
    public IDbConnection dbcon; // do zamykania i otwierania po��czenia

    public void CloseConnection(IDbConnection dbcon)
    {
        // Close connection
        dbcon.Close();

    }
    public void Insert(string query) 
    {
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = query;
        cmnd.ExecuteNonQuery();
    }
    public void UpdateDB(string query) {
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = query;
        cmnd.ExecuteNonQuery();
    }
    public IDataReader Select(string query) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }
    private void OnDestroy()
    {
        dbcon.Close();
    }
    public void Close()
    {
        dbcon.Close();
    }
    void Start()
    {
        //------------------------------- Wst�p
        // Je�li dobrze rozumiem, teraz jak podepniesz skrypt DBConnector to z automatu masz po��czenie z baz�
        // Ogarnijcie sobie skrypt clickOnMainButton - potrzebujesz tylko wiedzie� do jakiego elementu jest baza podpi�ta - a potem juz korzystasz z gotowych funkcji
        //------------------------------- Koniec wst�pu
        string connection = "URI=file:" + Application.dataPath + "/Database/GrzybiarzeDatabase.db";
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
        //------------------------------ SELECT i wyswietlanie
/*        IDataReader selectAllFromPlayer=Select("SELECT * FROM Testowa");
        while (selectAllFromPlayer.Read())
        {
            Debug.Log("id: " + selectAllFromPlayer[0].ToString());
            Debug.Log("Pesos: " + selectAllFromPlayer[1].ToString());
        }*/
        //-------------------------------
    }

}
