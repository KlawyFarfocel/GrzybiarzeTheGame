using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class addstat : MonoBehaviour
{
    public GameObject textmeshpro_text;

    TextMeshProUGUI textmeshpro_text_text;

    void OnMouseDown()
    {
        textmeshpro_text_text = textmeshpro_text.GetComponent<TextMeshProUGUI>();

        string a =textmeshpro_text_text.text;
    }
}
