using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public int dialogueSequence;

    public int seq_number=0;
    public List<string> portaitSpriteList;
    public List<string> dialogueTextList;
    public List<string> talkingPersonList;

    public GameObject DialoguePanel;
    public GameObject MoveToNextDialogueText;
    public GameObject TopPanel;
    public GameObject LinesController;

    public GameObject SectionText;
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
    private void AttachTopPanel(Scene scene, LoadSceneMode mode)
    {
        try
        {
            this.TopPanel = GameObject.Find("TopPanel");
        }
        catch
        {

        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += AttachTopPanel;
    }
    public void HandleDialogueChange(string spritePath,string dialogueText, string dialoguePerson)
    {
        GameObject.Find("TalkingPortret").GetComponent<Image>().sprite=Resources.Load<Sprite>(spritePath);
        GameObject.Find("DialoguePerson").GetComponent<TextMeshProUGUI>().text = dialoguePerson;
        GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>().text = dialogueText;
    }
    public void ToggleDialoguePanel(bool state)
    {
        DialoguePanel.SetActive(state);
        MoveToNextDialogueText.SetActive(state);
        LinesController.SetActive(state);
        TopPanel.SetActive(!state);
        try
        {
            LevelManager lvlManager=GameObject.Find("LevelManager").GetComponent<LevelManager>();
            GameObject.Find("SectionText").GetComponent<TextMeshProUGUI>().text = $"Poziom:<br>{lvlManager.currentClicks}/{lvlManager.clicksTarget}";
        }
        catch
        {

        }
    }
    public void FindDialogue(int scene_id)
    {
        DBConnector dbCon = GameObject.Find("Main Camera").GetComponent<DBConnector>();
        IDataReader getDialogues = dbCon.Select($"SELECT talkingPerson,dialogueText,portraitSprite FROM dialogues WHERE scene_id={scene_id} ORDER BY seq_number");
        while(getDialogues.Read())
        {
            talkingPersonList.Add("["+getDialogues[0].ToString()+"]");
            dialogueTextList.Add(getDialogues[1].ToString());
            portaitSpriteList.Add(getDialogues[2].ToString());
        }
        HandleDialogueChange(portaitSpriteList[0], dialogueTextList[0], talkingPersonList[0]); //odpala dialog
    }
    public void FindDialogue(string scene_id) //stringi s¹ do bossów
    {
        DBConnector dbCon = GameObject.Find("DialogueManager").GetComponent<DBConnector>();
        IDataReader getDialogues = dbCon.Select($"SELECT talkingPerson,dialogueText,portraitSprite FROM dialogues WHERE scene_id='{scene_id}' ORDER BY seq_number");
        while (getDialogues.Read())
        {
            talkingPersonList.Add("[" + getDialogues[0].ToString() + "]");
            dialogueTextList.Add(getDialogues[1].ToString());
            portaitSpriteList.Add(getDialogues[2].ToString());
        }
        if(dialogueTextList.Count > 0)
        {
            HandleDialogueChange(portaitSpriteList[0], dialogueTextList[0], talkingPersonList[0]); //odpala dialog
        }
        
    }
    IEnumerator goToNextLineAfterTime(float seconds,string param)
    {
        yield return new WaitForSeconds(seconds);
        if (param == "nextLine")
        {
            GoToNextLine();
        }
        else
        {
            ToggleDialoguePanel(true);
            FindDialogue(param);
        }
        
    }
    public void GoToNextLine()
    {
        seq_number++;
        if(seq_number<portaitSpriteList.Count)
        {
            HandleDialogueChange(portaitSpriteList[seq_number], dialogueTextList[seq_number], talkingPersonList[seq_number]);
            if (dialogueTextList[seq_number] == "Nigdy Ci nie wybaczê, nigdy! <umiera>")
            {
                GameObject.Find("DeathScreenEnd").GetComponent<Animation>().Play();
                StartCoroutine(goToNextLineAfterTime(0.5f,"nextLine"));
                StartCoroutine(goToNextLineAfterTime(3.5f, "end2"));    
            }
        }
        else
        {
            ToggleDialoguePanel(false);
            portaitSpriteList.Clear();
            dialogueTextList.Clear();
            talkingPersonList.Clear();
            seq_number = 0;
        }
    }
}
