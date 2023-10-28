using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        public int level;
        public int currentClicks;
        public int clicksTarget;
        private BackgroundManager bgManager;
        // Start is called before the first frame update
    void Start()
    {
        level = 1;
        currentClicks = 1;
    }
    private void Awake()
    {
        if (instance == null)
        {
                instance = this;
                DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(transform.root.gameObject);
        }
    }
    public void handleClicks()
        {
            if(currentClicks<clicksTarget)
            {
                currentClicks++;
            GameObject.Find("LevelProgress").GetComponent<TextMeshProUGUI>().text = $"Poziom: {level} <br> Etap: {currentClicks}/{clicksTarget}";
            }
            else
            {
                bgManager = GameObject.Find("Background").GetComponent<BackgroundManager>();
                level++;
                currentClicks = 1;
                GameObject.Find("LevelProgress").GetComponent<TextMeshProUGUI>().text = $"Poziom: {level} <br> Etap: {currentClicks}/{clicksTarget}";
                bgManager.changeValues(level);
            }
        }
    }
