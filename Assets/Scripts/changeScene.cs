using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void moveToNextScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        Debug.Log("dupa");
    }
}
