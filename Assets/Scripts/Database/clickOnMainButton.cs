using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickOnMainButton : MonoBehaviour
{

    private DBConnector dbConnector;
    // Start is called before the first frame update
    public void ClickHandler()
    {
        dbConnector.Insert("INSERT INTO Testowa (Pesos) VALUES (1683)");
        Debug.Log("Dodano");
    }
    void Start()
    {
        dbConnector = GameObject.Find("Las").GetComponent<DBConnector>();
    }
}
