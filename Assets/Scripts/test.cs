using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        var a = SceneController.Instance.inventoryManager.slots;
        Debug.Log(a.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
