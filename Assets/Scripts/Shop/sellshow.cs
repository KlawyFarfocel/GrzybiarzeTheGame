using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class sellshow : MonoBehaviour
{
    public GameObject textmeshpro_text;

    TextMeshProUGUI textmeshpro_text_text;
    // Start is called before the first frame update
    void Start()
    {
        textmeshpro_text_text = textmeshpro_text.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        string wal = File.ReadAllText(Application.dataPath + "/Model/value.txt");
        string wal_add = File.ReadAllText(Application.dataPath + "/Model/valuetoadd.txt");
        textmeshpro_text_text.text = wal+" (+"+wal_add+")";
    }
}
