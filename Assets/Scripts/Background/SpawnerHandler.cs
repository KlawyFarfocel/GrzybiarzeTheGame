using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpawnerHandler : MonoBehaviour
{
    void Start()
    {
    }
    public void handleClick()
    {
        LevelManager levelManager=GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.handleClicks();
    }
}
