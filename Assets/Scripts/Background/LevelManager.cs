using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
    {
        private bool naTeraz = false;
        public static LevelManager instance;
        public int level;
        public int currentClicks;
        public int clicksTarget;
        public bool wasOnMap;
        public bool hasSpawnedBoss = false;
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
    private void BackFromMap(Scene scene, LoadSceneMode mode)
    {
/*        if (wasOnMap)
        {
            DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
            dialogueManager.ToggleDialoguePanel(true);
            dialogueManager.FindDialogue("b" + level);

           

            spawnEnemy = GameObject.Find("EnemySpawner").GetComponent<SpawnEnemy>();
            spawnEnemy.TrySpawnEnemy(level + 1);
            wasOnMap = false;
        }*/
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += BackFromMap;
    }
    void Start()
    {
        if(!wasOnMap)
        {
            level = 1;
            currentClicks = 0;
        }
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
    IEnumerator LoadSceneWithAnimation()
    {
        // Find the LoadingScreen GameObject and get its Animation component
        Animation loadingScreenAnimation = GameObject.Find("LoadingScreen").GetComponent<Animation>();

        // Play the fade animation
        loadingScreenAnimation.Play();

        // Wait for the animation to finish
        yield return new WaitForSeconds(loadingScreenAnimation.clip.length-0.03f);

        // Load the next scene
        SceneManager.LoadScene(6);
    }
    public void handleClicks()
        {
            if(GameObject.Find("Enemy(Clone)") == null){
                if (currentClicks == 2 && level==1)
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
                    try
                    {
                        GameObject.Find("SectionText").GetComponent<TextMeshProUGUI>().text = $"Poziom:<br>{currentClicks}/{clicksTarget}";
                    }
                    catch { }
                        
                    }
                }
                else if(currentClicks == clicksTarget)
            {
                if(!hasSpawnedBoss)
                {
                    DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
                    dialogueManager.ToggleDialoguePanel(true);
                    dialogueManager.FindDialogue("b" + (level + 1));

                    spawnEnemy = GameObject.Find("EnemySpawner").GetComponent<SpawnEnemy>();
                    spawnEnemy.TrySpawnEnemy(level + 2);

                    hasSpawnedBoss = true;
                }
                else
                {
                    currentClicks++;
                }
            }
            if (currentClicks > clicksTarget)
            {
                hasSpawnedBoss = false;
                bgManager = GameObject.Find("Background").GetComponent<BackgroundManager>();
                level++;
                currentClicks = 0;
                GameObject.Find("LoadingScreen").GetComponent<Animation>().Play();
                Player player = GetComponent<Player>();
                player.CURRENT_HP = player.HP;
                GameObject.Find("SectionText").GetComponent<TextMeshProUGUI>().text = $"Poziom:<br>{currentClicks}/{clicksTarget}";
                StartCoroutine(LoadSceneWithAnimation());
            }
        }     
    }
    }
