using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [Header("Text Files")]
    [SerializeField] private TextAsset hungerText;
    [SerializeField] private TextAsset thirstText;
    [SerializeField] private TextAsset toiletText;
    [SerializeField] private TextAsset sanityText;

    // hunger data
    private List<string> hungerDialogue50 = new();
    private List<string> hungerDialogue20 = new();

    // thirst data
    private List<string> thirstDialogue50 = new();
    private List<string> thirstDialogue20 = new();

    // toilet data
    private List<string> toiletDialogue50 = new();
    private List<string> toiletDialogue20 = new();

    // sanity data
    private List<string> sanityDialogue80 = new();
    private List<string> sanityDialogue50 = new();
    private List<string> sanityDialogue20 = new();
    private List<string> sanityDialogue10 = new();

    private StringReader fileReader;
    private string fileLine, dataSection;

    public static DataLoader instance {get; private set;}

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
        parseHungerData();
        parseThirstData();
        parseToiletData();
        parseSanityData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void parseHungerData()
    {
        fileReader = new StringReader(hungerText.text);

        while((fileLine = fileReader.ReadLine()) != null)
        {
            fileLine = fileLine.Trim();
            if(fileLine == "") continue;

            if(fileLine == "50" || fileLine == "20"){

                dataSection = fileLine;
                // Debug.Log("dataSection: " + dataSection);
            }

            else if(dataSection == "50")
            {
                hungerDialogue50.Add(fileLine);
            }

            else if(dataSection == "20")
            {
                hungerDialogue20.Add(fileLine);
            }
        }
    }

    private void parseThirstData()
    {
        fileReader = new StringReader(thirstText.text);

        while((fileLine = fileReader.ReadLine()) != null)
        {
            fileLine = fileLine.Trim();
            if(fileLine == "") continue;

            if(fileLine == "50" || fileLine == "20"){

                dataSection = fileLine;
                // Debug.Log("dataSection: " + dataSection);
            }
            else if(dataSection == "50")
            {
                thirstDialogue50.Add(fileLine);
            }

            else if(dataSection == "20")
            {
                thirstDialogue20.Add(fileLine);
            }
        }
    }

    private void parseToiletData()
    {
        fileReader = new StringReader(toiletText.text);

        while((fileLine = fileReader.ReadLine()) != null)
        {
            fileLine = fileLine.Trim();
            if(fileLine == "") continue;

            if(fileLine == "50" || fileLine == "20"){

                dataSection = fileLine;
                // Debug.Log("dataSection: " + dataSection);
            }
            else if(dataSection == "50")
            {
                toiletDialogue50.Add(fileLine);
            }

            else if(dataSection == "20")
            {
                toiletDialogue20.Add(fileLine);
            }
        }
    }

    private void parseSanityData()
    {
        fileReader = new StringReader(sanityText.text);

        while((fileLine = fileReader.ReadLine()) != null)
        {
            fileLine = fileLine.Trim();
            if(fileLine == "") continue;

            if(fileLine == "50" || fileLine == "10"){

                dataSection = fileLine;
                // Debug.Log("dataSection: " + dataSection);
            }

            else if(dataSection == "50")
            {
                sanityDialogue50.Add(fileLine);
            }

            else if(dataSection == "10")
            {
                sanityDialogue10.Add(fileLine);
            }
        }
    }


    public void triggerHungerDialogue(int interval)
    {
        int randomIndex; 
        string toPrint = "invalid";
        
        if(interval == 20)
        {
            randomIndex = (hungerDialogue20.Count == 1) ?  0 : Random.Range(0, hungerDialogue20.Count);
            toPrint = hungerDialogue20[randomIndex];
            Debug.Log("toPrint 20: " + toPrint);
        }
        else if(interval == 50)
        {
            randomIndex = (hungerDialogue50.Count == 1) ?  0 : Random.Range(0, hungerDialogue50.Count);
            toPrint = hungerDialogue50[randomIndex];
            Debug.Log("toPrint 50: " + toPrint);
        }
        
        
        Dialogue.instance.addToDialogue(toPrint);
    }

    public void triggerThirstDialogue(int interval)
    {
        int randomIndex; 
        string toPrint = "invalid";
        
        if(interval == 20)
        {
            randomIndex = (thirstDialogue20.Count == 1) ?  0 : Random.Range(0, thirstDialogue20.Count);
            toPrint = thirstDialogue20[randomIndex];
            Debug.Log("toPrint 20: " + toPrint);
        }
        else if(interval == 50)
        {
            randomIndex = (thirstDialogue50.Count == 1) ?  0 : Random.Range(0, thirstDialogue50.Count);
            toPrint = thirstDialogue50[randomIndex];
            Debug.Log("toPrint 50: " + toPrint);
        }
        
        Dialogue.instance.addToDialogue(toPrint);
    }

    public void triggerToiletDialogue(int interval)
    {
        int randomIndex; 
        string toPrint = "invalid";
        
        if(interval == 20)
        {
            randomIndex = (toiletDialogue20.Count == 1) ?  0 : Random.Range(0, toiletDialogue20.Count);
            toPrint = toiletDialogue20[randomIndex];
            Debug.Log("toPrint 20: " + toPrint);
        }
        else if(interval == 50)
        {
            randomIndex = (toiletDialogue50.Count == 1) ?  0 : Random.Range(0, toiletDialogue50.Count);
            toPrint = toiletDialogue50[randomIndex];
            Debug.Log("toPrint 50: " + toPrint);
        }
        
        Dialogue.instance.addToDialogue(toPrint);
    }

    public void triggerSanityDialogue(int interval)
    {
        int randomIndex; 
        string toPrint = "invalid";
        
        if(interval == 10)
        {
            randomIndex = (sanityDialogue10.Count == 1) ?  0 : Random.Range(0, sanityDialogue10.Count);
            toPrint = sanityDialogue10[randomIndex];
            Debug.Log("toPrint 20: " + toPrint);
        }
        else if(interval == 50)
        {
            randomIndex = (sanityDialogue50.Count == 1) ?  0 : Random.Range(0, sanityDialogue50.Count);
            toPrint = sanityDialogue50[randomIndex];
            Debug.Log("toPrint 50: " + toPrint);
        }
        
        Dialogue.instance.addToDialogue(toPrint);
    }

    

}
