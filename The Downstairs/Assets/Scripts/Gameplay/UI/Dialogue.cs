using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue File")]
    [SerializeField] private TextAsset testing;

    [Header("Dialogue Appearance")]
    [SerializeField] private TextMeshProUGUI dialogueUI;
    [SerializeField] private bool fadeInAnimation;

    [Header("Printing Animation")]
    [SerializeField] private float clearTime;
    [SerializeField] private float printSpeed;
    [SerializeField] private float printDisplaySeconds;

    [Header("Fading Animation")]
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float fadeDisplayTime;
    [SerializeField] private float minTextAlpha, maxTextAlpha;
    private string[] fileLines;

    private Color newTextColor = new();
    private bool isPrinting;
    private float secondsPassed;
    private Queue<string> dialogueQueue = new();


    public static Dialogue instance {get; private set;}

    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }
        else{
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // currLineIndex = 0;
        // fileLines = testing.text.Split('\n');

        // StartCoroutine(playIntroDialogue());

        isPrinting = false;

        newTextColor = dialogueUI.color;

        secondsPassed = 0;

        clearDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueQueue.Count != 0 && !isPrinting)
        {
            if(fadeInAnimation)
            {
                StartCoroutine(fadeIntoDialogue(dialogueQueue.Dequeue()));
            }
            else
            {
                StartCoroutine(printCharByChar(dialogueQueue.Dequeue()));
            }
            
        }
    }

    public IEnumerator playIntroDialogue(){

        foreach(string text in fileLines){

            StartCoroutine(printCharByChar(text));

            yield return new WaitForSeconds(3);

            clearDialogue();
        }
        
    }

    private IEnumerator printCharByChar(string toPrint){
        isPrinting = true;
        foreach(char character in toPrint)
        {
            dialogueUI.text += character;
            
            yield return new WaitForSeconds(printSpeed);
        }

        yield return new WaitForSeconds(printDisplaySeconds);
        clearDialogue();

        isPrinting = false;
        
    }

    private IEnumerator fadeIntoDialogue(string toPrint)
    {
        isPrinting = true;

        clearDialogue();

        secondsPassed = 0;

        dialogueUI.text = toPrint;

        while(secondsPassed < fadeInTime)
        {
            newTextColor.a = Mathf.Lerp(minTextAlpha, maxTextAlpha, secondsPassed/fadeInTime);
            dialogueUI.color = newTextColor;

            secondsPassed += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(fadeDisplayTime);

        secondsPassed = 0;

        while(secondsPassed < fadeInTime + fadeOutTime)
        {
            newTextColor.a = Mathf.Lerp(maxTextAlpha, minTextAlpha, secondsPassed/fadeOutTime);
            dialogueUI.color = newTextColor;

            secondsPassed += Time.deltaTime;

            yield return null;
        }

        isPrinting = false;
        
    }

    private void clearDialogue(){

        dialogueUI.text = "";
    }

    public void addToDialogue(string newDialogue)
    {
        dialogueQueue.Enqueue(newDialogue);
    }

    
        
}
