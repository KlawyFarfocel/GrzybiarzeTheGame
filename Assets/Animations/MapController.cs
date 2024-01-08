using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public GameObject[] locations;
    // Start is called before the first frame update
    void Start()
    {
       int level=GameObject.Find("LevelManager").GetComponent<LevelManager>().level;
       GameObject.Find("LevelManager").GetComponent<LevelManager>().wasOnMap = true;
        if (level == 2)
        {
            locations[1].SetActive(true);
        }
        else if(level == 3) //Jedziemy na Gotlandie
        {
            markPointAsVisited(locations[0]);
            setPath(locations[1]);
            locations[3].SetActive(true);
        }
        else if (level == 4) // Jedziemy na Gibraltar
        {
            markPointAsVisited(locations[0]);
            setPath(locations[1]);
            markPointAsVisited(locations[2]);
            setPath(locations[3]);
            markPointAsVisited(locations[4]);
            locations[5].SetActive(true);
        }else if(level == 5) // I cyk do Rumunii
        {
            markPointAsVisited(locations[0]);
            setPath(locations[1]);
            markPointAsVisited(locations[2]);
            setPath(locations[3]);
            markPointAsVisited(locations[4]);
            setPath(locations[5]);
            markPointAsVisited(locations[6]);
            locations[7].SetActive(true);
        }
    }
    void markPointAsVisited(GameObject obj)
    {
        obj.transform.Find(obj.name + "__Green").GetComponent<Image>().color=new Color(1f,1f,1f,1f);
    }
    void setPath(GameObject obj)
    {
        obj.SetActive(true);
        obj.GetComponent<Animator>().enabled=false;


        GameObject.Find(obj.name).GetComponent<Renderer>().material.SetFloat("_FillingValue", 0.0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
