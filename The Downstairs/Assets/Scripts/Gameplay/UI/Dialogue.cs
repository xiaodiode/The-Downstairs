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
    [SerializeField] private bool fadeInAnimation;
    private EnterFont dialogueDilation;

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
        // StartCoroutine(playIntroDialogue());

        isPrinting = false;

        dialogueDilation = dialogueUI.gameObject.GetComponent<EnterFont>();

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
        // Debug.Log("dialogue queue count: " + dialogueQueue.Count);
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

        dialogueDilation.StartDilation();

        while(secondsPassed < fadeInTime)
        {
            newTextColor.a = Mathf.Lerp(minTextAlpha, maxTextAlpha, secondsPassed/fadeInTime);
            dialogueUI.color = newTextColor;

            secondsPassed += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(fadeDisplayTime);

        secondsPassed = 0;

        dialogueDilation.EndDilation();

        while(secondsPassed < fadeInTime + fadeOutTime)
        {
            newTextColor.a = Mathf.Lerp(maxTextAlpha, minTextAlpha, secondsPassed/fadeOutTime);
            dialogueUI.color = newTextColor;

            secondsPassed += Time.deltaTime;

            yield return null;
        }

        isPrinting = false;
        
    }

    private void clearDialogue()
    {
        dialogueUI.text = "";
    }

    public void addToDialogue(string newDialogue)
    {
        dialogueQueue.Enqueue(newDialogue);
    }


    public void triggerHungerDialogue(int interval)
    {
        int randomIndex; 
        string toPrint = "invalid";

        switch(interval)
        {
            case 20:
                randomIndex = (DataLoader.instance.hungerDialogue20.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.hungerDialogue20.Count);
                toPrint = DataLoader.instance.hungerDialogue20[randomIndex];
                Debug.Log("toPrint 20: " + toPrint);

                break;

            case 50:
                randomIndex = (DataLoader.instance.hungerDialogue50.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.hungerDialogue50.Count);
                toPrint = DataLoader.instance.hungerDialogue50[randomIndex];
                Debug.Log("toPrint 50: " + toPrint);

                break;
        }   
        
        addToDialogue(toPrint);
    }

    public void triggerThirstDialogue(int interval)
    {
        int randomIndex; 
        string toPrint = "invalid";

        switch(interval)
        {
            case 20:
                randomIndex = (DataLoader.instance.thirstDialogue20.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.thirstDialogue20.Count);
                toPrint = DataLoader.instance.thirstDialogue20[randomIndex];
                Debug.Log("toPrint 20: " + toPrint);

                break;

            case 50:
                randomIndex = (DataLoader.instance.thirstDialogue50.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.thirstDialogue50.Count);
                toPrint = DataLoader.instance.thirstDialogue50[randomIndex];
                Debug.Log("toPrint 50: " + toPrint);

                break;
        }
        
        addToDialogue(toPrint);
    }

    public void triggerToiletDialogue(int interval)
    {
        int randomIndex; 
        string toPrint = "invalid";
        
        switch(interval)
        {
            case 20:
                randomIndex = (DataLoader.instance.toiletDialogue20.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.toiletDialogue20.Count);
                toPrint = DataLoader.instance.toiletDialogue20[randomIndex];
                Debug.Log("toPrint 20: " + toPrint);

                break;

            case 50: 
                randomIndex = (DataLoader.instance.toiletDialogue50.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.toiletDialogue50.Count);
                toPrint = DataLoader.instance.toiletDialogue50[randomIndex];
                Debug.Log("toPrint 50: " + toPrint);

                break;
        }

        addToDialogue(toPrint);
    }

    public void triggerSanityDialogue(int interval)
    {
        int randomIndex; 
        string toPrint = "invalid";
        
        switch(interval)
        {
            case 10:
                randomIndex = (DataLoader.instance.sanityDialogue10.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.sanityDialogue10.Count);
                toPrint = DataLoader.instance.sanityDialogue10[randomIndex];
                Debug.Log("toPrint 10: " + toPrint);
                
                break;

            case 50:
                randomIndex = (DataLoader.instance.sanityDialogue50.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.sanityDialogue50.Count);
                toPrint = DataLoader.instance.sanityDialogue50[randomIndex];
                Debug.Log("toPrint 50: " + toPrint);

                break;
        }
        
        addToDialogue(toPrint);
    }

    public void triggerEmptyFridgeDialogue()
    {
        int randomIndex;
        string toPrint;

        randomIndex = (DataLoader.instance.emptyFridgeDialogue.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.emptyFridgeDialogue.Count);
        toPrint = DataLoader.instance.emptyFridgeDialogue[randomIndex];

        addToDialogue(toPrint);
    }

    public void triggerEmptyPitcherDialogue()
    {
        int randomIndex;
        string toPrint;

        randomIndex = (DataLoader.instance.emptyPitcherDialogue.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.emptyPitcherDialogue.Count);
        toPrint = DataLoader.instance.emptyPitcherDialogue[randomIndex];

        addToDialogue(toPrint);
    }

    public void triggerLaundryDialogue(string state)
    {
        int randomIndex;
        string toPrint = "invalid";

        switch(state)
        {
            case "empty":
                randomIndex = (DataLoader.instance.emptyLaundryDialogue.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.emptyLaundryDialogue.Count);
                toPrint = DataLoader.instance.emptyLaundryDialogue[randomIndex];

                break;

            case "unused":
                randomIndex = (DataLoader.instance.unusedLaundryDialogue.Count == 1) ?  0 : Random.Range(0, DataLoader.instance.unusedLaundryDialogue.Count);
                toPrint = DataLoader.instance.unusedLaundryDialogue[randomIndex];

                break;
        }

        addToDialogue(toPrint);
    }
        
}
