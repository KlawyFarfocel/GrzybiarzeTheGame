using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player player = GameObject.Find("LevelManager").GetComponent<Player>();
        player.HandleUIChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
