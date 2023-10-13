using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class ClickOnMush : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void OnClick()
    {
        string name = gameObject.name;
        Debug.Log(name + "Dodano do eq");
    }
}
