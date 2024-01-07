using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAspect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float aspect=Screen.width/(float)Screen.height;
        float newScaleX=aspect*22.22f;
        GameObject.Find("Got-Gib").GetComponent<Transform>().localScale = new Vector3(newScaleX, 10, 10);
        float newScaleForPoints = newScaleX / 10;

        LineRenderer lineRenderer = GameObject.Find("Got-Gib").GetComponent<LineRenderer>();
        Vector3[] positions=new Vector3[500];
        lineRenderer.GetPositions(positions);
        for(int i = 0; i < positions.Length; i++)
        {
            positions[i].x = positions[i].x * newScaleForPoints;
        }
        lineRenderer.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
