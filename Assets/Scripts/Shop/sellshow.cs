using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using TMPro;
using UnityEngine;

public class sellshow : MonoBehaviour
{
    private DBConnector dbConnector;
    public GameObject textmeshpro_text;

    TextMeshProUGUI textmeshpro_text_text;
    // Start is called before the first frame update
    void Start()
    {
        dbConnector = GameObject.Find("TopBar").GetComponent<DBConnector>();
        textmeshpro_text_text = textmeshpro_text.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        IDataReader selectAllFromPlayer = dbConnector.Select("SELECT money,moneytoadd FROM postac");
        while (selectAllFromPlayer.Read())
        {
            //  Debug.Log(selectAllFromPlayer.ToString()) ;
      
                string wal = selectAllFromPlayer[0].ToString();
            string wal_add= selectAllFromPlayer[1].ToString();
            textmeshpro_text_text.text = wal + " (+" + wal_add + ")";
        }
        //string wal = File.ReadAllText(Application.dataPath + "/Model/value.txt");
       // string wal_add = File.ReadAllText(Application.dataPath + "/Model/valuetoadd.txt");
       // textmeshpro_text_text.text = wal+" (+"+wal_add+")";
    }
}
