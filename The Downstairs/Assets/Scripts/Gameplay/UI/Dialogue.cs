using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue File")]
    [SerializeField] private TextAsset testing;

    [Header("Dialogue Appearance")]
    [SerializeField] private TextMeshProUGUI dialogueUI;

    [Header("Printing Animation")]
    [SerializeField] private float clearTime;
    [SerializeField] private float printSpeed;

    private string[] fileLines;
    private bool isReset, isFinished, finishPrinting;
    private int currLineIndex;


    // Start is called before the first frame update
    void Start()
    {
        currLineIndex = 0;
        fileLines = testing.text.Split('\n');

        clearDialogue();

        StartCoroutine(playIntroDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator playIntroDialogue(){

        foreach(string text in fileLines){

            StartCoroutine(printToDialogue(text));

            yield return new WaitForSeconds(3);

            clearDialogue();
        }
        
    }

    private IEnumerator printToDialogue(string toPrint){
        isReset = false;
        foreach(char character in toPrint){
            dialogueUI.text += character;
            
            if(finishPrinting){
                dialogueUI.text = toPrint;
                break;
            }
            else{
                yield return new WaitForSeconds(printSpeed);
            }
        }

        finishPrinting = false;
        isFinished = true;
        
    }

    private void clearDialogue(){

        dialogueUI.text = "";
    }

    
        
}
