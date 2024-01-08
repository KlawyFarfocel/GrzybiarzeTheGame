using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System.IO;

public class DBConnector : MonoBehaviour
{
    // Use this for initialization
    public IDbConnection dbcon; // do zamykania i otwierania po³¹czenia

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
        try
        {
            dbcon.Close();
        }
        catch
        {

        }
        
    }
    public void Close()
    {
        dbcon.Close();
    }
    void Start()
    {
        //------------------------------- Wstêp
        // Jeœli dobrze rozumiem, teraz jak podepniesz skrypt DBConnector to z automatu masz po³¹czenie z baz¹
        // Ogarnijcie sobie skrypt clickOnMainButton - potrzebujesz tylko wiedzieæ do jakiego elementu jest baza podpiêta - a potem juz korzystasz z gotowych funkcji
        //------------------------------- Koniec wstêpu

        #if UNITY_EDITOR
            string connection = "URI=file:" + Application.dataPath + "\\StreamingAssets" + "/" + "Database/GrzybiarzeDatabase.db"; //Path to database

#elif UNITY_ANDROID
string dbName = "GrzybiarzeDatabase.db";
string dbPath = Path.Combine(Application.streamingAssetsPath, "Database", dbName);
string destinationPath = Path.Combine(Application.persistentDataPath, dbName);

// Check if the database exists in the destination folder
if (!File.Exists(destinationPath))
{
    // If it doesn't exist, load the file using WWW
    Debug.Log("Database does not exist in destination. Copying...");

    WWW www = new WWW(dbPath);
    while (!www.isDone) { /* Wait for the file to be loaded */ }

    // Write the loaded data to the destination path
    File.WriteAllBytes(destinationPath, www.bytes);

    Debug.Log("Database copied successfully.");
}
else
{
    Debug.Log("Database already exists in destination. No need to copy.");
}

string connection = "URI=file:" + destinationPath;

#endif
        //string connection = "URI=file:" + Application.dataPath + "/Database/GrzybiarzeDatabase.db";
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
