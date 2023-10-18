using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class sell : MonoBehaviour
{
    // Start is called before the first frame update
    public void add()
    {
        Debug.Log('d');
        string wal = File.ReadAllText(Application.dataPath + "/Model/value.txt");
        string wal_add = File.ReadAllText(Application.dataPath + "/Model/valuetoadd.txt");

        int V=int.Parse(wal);
        int V_add=int.Parse(wal_add);
        int wynik=V+V_add;
        File.WriteAllText(Application.dataPath + "/Model/value.txt",wynik.ToString());
        File.WriteAllText(Application.dataPath + "/Model/valuetoadd.txt", "0");
    }
}
