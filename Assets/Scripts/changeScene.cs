using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void moveToNextScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
