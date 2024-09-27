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
    private List<string> hungerDialogue10 = new();

    // thirst data
    private List<string> thirstDialogue50 = new();
    private List<string> thirstDialgoue10 = new();

    // toilet data
    private List<string> toiletDialogue50 = new();
    private List<string> toiletDialogue10 = new();

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void parseHungerData(){

        fileReader = new StringReader(hungerText.text);

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
                hungerDialogue50.Add(fileLine);
            }

            else if(dataSection == "10")
            {
                hungerDialogue10.Add(fileLine);
            }
        }
    }


    public void triggerHungerDialogue(int interval)
    {
        int randomIndex; 
        string toPrint = "invalid";
        
        if(interval == 50){
            randomIndex = (hungerDialogue50.Count == 1) ?  0 : Random.Range(0, hungerDialogue50.Count);
            toPrint = hungerDialogue50[randomIndex];
            Debug.Log("toPrint 50: " + toPrint);
        }
        else if(interval == 10)
        {
            randomIndex = (hungerDialogue10.Count == 1) ?  0 : Random.Range(0, hungerDialogue10.Count);
            toPrint = hungerDialogue10[randomIndex];
            Debug.Log("toPrint 10: " + toPrint);
        }
        
        Dialogue.instance.addToDialogue(toPrint);
    }

}
