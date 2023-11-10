using System.IO;
using UnityEngine;
//[CreateAssetMenu(fileName = "Nowy przedmiot", menuName = "Ekwipunek/NowyPrzedmiot")]
//[System.Serializable]
public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public Sprite Sprite;
    public int Value;
    public float RespChange;
    public string sprite;

    public GameObject prefab;
}
