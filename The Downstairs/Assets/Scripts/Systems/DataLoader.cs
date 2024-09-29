using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class DataLoader : MonoBehaviour
{
    [Header("Text Files")]

    [Header("Meters")]
    [SerializeField] private TextAsset hungerText;
    [SerializeField] private TextAsset thirstText;
    [SerializeField] private TextAsset toiletText;
    [SerializeField] private TextAsset sanityText;

    [Header("Resources")]
    [SerializeField] private TextAsset emptyFridgeText;
    [SerializeField] private TextAsset emptyPitcherText;
    [SerializeField] private TextAsset laundryText;

    // hunger data
    public List<string> hungerDialogue50 = new();
    public List<string> hungerDialogue20 = new();

    // thirst data
    public List<string> thirstDialogue50 = new();
    public List<string> thirstDialogue20 = new();

    // toilet data
    public List<string> toiletDialogue50 = new();
    public List<string> toiletDialogue20 = new();

    // sanity data
    public List<string> sanityDialogue80 = new();
    public List<string> sanityDialogue50 = new();
    public List<string> sanityDialogue20 = new();
    public List<string> sanityDialogue10 = new();

    // empty fridge data
    public List<string> emptyFridgeDialogue = new();

    // empty water pitcher data
    public List<string> emptyPitcherDialogue = new();

    // laundry data
    public List<string> emptyLaundryDialogue = new();
    public List<string> unusedLaundryDialogue = new();

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
        // parsing meter dialogue data
        parseHungerData();
        parseThirstData();
        parseToiletData();
        parseSanityData();

        // parsing resource dialogue data
        parseEmptyFridgeData();
        parseEmptyPitcherData();
        parseLaundryData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void parseHungerData()
    {
        StringReader fileReader = new StringReader(hungerText.text);

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
        StringReader fileReader = new StringReader(thirstText.text);

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
        StringReader fileReader = new StringReader(toiletText.text);

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
        StringReader fileReader = new StringReader(sanityText.text);

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

    private void parseEmptyFridgeData()
    {
        StringReader fileReader = new StringReader(emptyFridgeText.text);
        
        while((fileLine = fileReader.ReadLine()) != null)
        {
            fileLine = fileLine.Trim();
            if(fileLine == "") continue;

            emptyFridgeDialogue.Add(fileLine);
        }
    }

    private void parseEmptyPitcherData()
    {
        StringReader fileReader = new StringReader(emptyPitcherText.text);

        while((fileLine = fileReader.ReadLine()) != null)
        {
            fileLine = fileLine.Trim();
            if(fileLine == "") continue;

            emptyPitcherDialogue.Add(fileLine);
        }
    }

    private void parseLaundryData()
    {
        StringReader fileReader = new StringReader(laundryText.text);

        while((fileLine = fileReader.ReadLine()) != null)
        {
            fileLine = fileLine.Trim();
            if(fileLine == "") continue;

            if(fileLine == "empty" || fileLine == "unused"){

                dataSection = fileLine;
                // Debug.Log("dataSection: " + dataSection);
            }

            else if(dataSection == "empty")
            {
                emptyLaundryDialogue.Add(fileLine);
            }

            else if(dataSection == "unused")
            {
                unusedLaundryDialogue.Add(fileLine);
            }
        }
    }


    

    

}
