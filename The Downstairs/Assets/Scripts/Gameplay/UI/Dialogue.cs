using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue File")]
    [SerializeField] private TextAsset testing;

    [Header("Dialogue Appearance")]
    [SerializeField] private TextMeshProUGUI dialogueUI;

    [Header("Printing Animation")]
    [SerializeField] private float clearTime;
    [SerializeField] private float printSpeed;
    [SerializeField] private float displaySeconds;
    private string[] fileLines;
    private bool isPrinting;

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
        isPrinting = false;

        // currLineIndex = 0;
        // fileLines = testing.text.Split('\n');

        clearDialogue();

        // StartCoroutine(playIntroDialogue());
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueQueue.Count != 0 && !isPrinting){
            StartCoroutine(printToDialogue(dialogueQueue.Dequeue()));
        }
    }

    public IEnumerator playIntroDialogue(){

        foreach(string text in fileLines){

            StartCoroutine(printToDialogue(text));

            yield return new WaitForSeconds(3);

            clearDialogue();
        }
        
    }

    private IEnumerator printToDialogue(string toPrint){
        isPrinting = true;
        foreach(char character in toPrint)
        {
            dialogueUI.text += character;
            
            yield return new WaitForSeconds(printSpeed);
        }

        yield return new WaitForSeconds(displaySeconds);
        clearDialogue();

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
