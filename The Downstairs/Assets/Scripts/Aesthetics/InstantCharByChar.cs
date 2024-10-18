using System.Collections;
using UnityEngine;
using TMPro;

public class InstantCharByChar : MonoBehaviour
{
    [Header("Text Animation Settings")]
    [SerializeField] private float printSpeed = 0.05f;
    [SerializeField] private float waitToPrint = 1.0f;

    private TextMeshProUGUI dialogueUI;
    private string originalText;

    private void Awake()
    { 
        dialogueUI = GetComponent<TextMeshProUGUI>();
        originalText = dialogueUI.text;
    }

    private void OnEnable()
    { 
        StartCoroutine(PrintText());
    }

    private IEnumerator PrintText()
    {
        dialogueUI.text = "";
        yield return new WaitForSeconds(waitToPrint);

        foreach (char c in originalText)
        {
            dialogueUI.text += c;  
            yield return new WaitForSeconds(printSpeed);  
        }
    }
}