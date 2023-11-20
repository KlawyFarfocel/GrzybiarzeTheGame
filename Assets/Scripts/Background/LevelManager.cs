using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

    public class LevelManager : MonoBehaviour
    {
        private bool naTeraz = false;
        public static LevelManager instance;
        public int level;
        public int currentClicks;
        public int clicksTarget;
         private BackgroundManager bgManager;
    public SpawnEnemy spawnEnemy;
    // Start is called before the first frame update

    T GetStateMachineBehaviour<T>(Animator animator, string stateName) where T : StateMachineBehaviour
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Check if the state name matches
        if (stateInfo.IsName(stateName))
        {
            // Loop through the behaviours of the current state
            foreach (var behaviour in animator.GetBehaviours<T>())
            {
                // Return the first one found
                return behaviour;
            }
        }

        return null;
    }

    void Start()
    {
        level = 1;
        currentClicks = 0;
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
            if(GameObject.Find("Enemy(Clone)") == null){
                if (currentClicks == 2)
                {
                    var dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
                    dialogueManager.ToggleDialoguePanel(true);
                    dialogueManager.FindDialogue(0);
                }

                if (currentClicks < clicksTarget)
                {
                    if (currentClicks == 3)
                    {
                        GameObject.Find("SectionText").GetComponent<TextMeshProUGUI>().text = $"Poziom:<br>{currentClicks}/{clicksTarget}";
                        currentClicks++;
                        naTeraz = true;
                    }
                    else
                    {
                        if (naTeraz)
                        {
                            GameObject.Find("SectionText").GetComponent<TextMeshProUGUI>().text = $"Poziom:<br>{currentClicks}/{clicksTarget}";
                            naTeraz = false;
                            return;
                        }
                        currentClicks++;
                        GameObject.Find("SectionText").GetComponent<TextMeshProUGUI>().text = $"Poziom:<br>{currentClicks}/{clicksTarget}";
                    }
                }
                else
                {
                    bgManager = GameObject.Find("Background").GetComponent<BackgroundManager>();
                    level++;
                    currentClicks = 0;
                    GameObject.Find("SectionText").GetComponent<TextMeshProUGUI>().text = $"Poziom:<br>{currentClicks}/{clicksTarget}";
                    GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>().text = $"Etap:<br>Las wydmowy";
                    bgManager.changeValues(level);

                    //po zmianie sceny resp enemy 
                    spawnEnemy = GameObject.Find("EnemySpawner").GetComponent<SpawnEnemy>();
                    spawnEnemy.TrySpawnEnemy(level + 1);
                }
            }     
    }
    }
