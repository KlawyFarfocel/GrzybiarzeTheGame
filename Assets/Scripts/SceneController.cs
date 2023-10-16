using UnityEngine;
using UnityEngine.UI;
public class SceneController : MonoBehaviour
{
    public InventoryManager inventoryManager;

    private static SceneController _instance;

    public static SceneController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneController>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SceneController");
                    _instance = go.AddComponent<SceneController>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);

            Debug.Log("SceneController Awake: " + gameObject.scene.name);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
