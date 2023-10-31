using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class sell : MonoBehaviour
{
    private DBConnector dbConnector;
    private InventoryManager items=InventoryManager.Instance; //tutaj jest ten eq co przechodzi miedzy scenami 
    // Start is called before the first frame update
    public void add()
    {
        string wal;
        string wal_add;
        IDataReader selectAllFromPlayer = dbConnector.Select("SELECT money,moneytoadd FROM postac");
        while (selectAllFromPlayer.Read())
        {
            //  Debug.Log(selectAllFromPlayer.ToString()) ;

             wal = selectAllFromPlayer[0].ToString();
             wal_add = selectAllFromPlayer[1].ToString();
            int V = int.Parse(wal);
            int V_add = int.Parse(wal_add);
            int wynik = V + V_add;
            string updateQuery = "UPDATE postac SET money = " + wynik;
            //Debug.Log(updateQuery);
            dbConnector.UpdateDB(updateQuery);
            string updateQuery1 = "UPDATE postac SET moneytoadd = 0";
            //Debug.Log(updateQuery);
            dbConnector.UpdateDB(updateQuery1);
        }
       /* int V=int.Parse(wal);
        int V_add=int.Parse(wal_add);
        int wynik=V+V_add;
        File.WriteAllText(Application.dataPath + "/Model/value.txt",wynik.ToString());
        File.WriteAllText(Application.dataPath + "/Model/valuetoadd.txt", "0");*/

        foreach (Slot slot in items.slots) //przepierdol sie po slotach i poodpinaj itemy
        {
            slot.RemoveItemFromSlot();
        }
    }
    public void Start()
    {
        dbConnector = GameObject.Find("TopBar").GetComponent<DBConnector>();
      
    }

    public void Update()
    {
        int xd = items.ForceValueEvaluation();
        Debug.Log(xd);
        string updateQuery = "UPDATE postac SET moneytoadd = " + xd;
        //Debug.Log(updateQuery);
        dbConnector.UpdateDB(updateQuery);
    }
}
