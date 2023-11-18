using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public Text textComponent;

    void Start()
    {
    }

    public void ShowTextEffect(string message)
    {
        // Ustaw tekst
        textComponent.text = message;

        // Uruchom animacjê
        StartCoroutine(ShowAndHide());
    }

    IEnumerator ShowAndHide()
    {
        // Poka¿ tekst
        textComponent.enabled = true;

        // Poczekaj przez np. 2 sekundy
        yield return new WaitForSeconds(3f);

        // Ukryj tekst
        textComponent.enabled = false;
    }
}
