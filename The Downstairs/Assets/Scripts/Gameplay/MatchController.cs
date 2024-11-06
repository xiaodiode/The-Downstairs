using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public bool matchesFull;
    public int currMatchCount;
    [SerializeField] private int startingMatchCount;
    [SerializeField] private int matchCap;
    [SerializeField] private int minMatchPickup, maxMatchPickup;
    [SerializeField] private List<GameObject> matchUI = new();

    private int randomCount;

    public static MatchController instance {get; private set;}

    // Start is called before the first frame update
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetMatches()
    {
        matchesFull = false;
        currMatchCount = startingMatchCount;

        for(int i = 0; i < currMatchCount; i++){
            matchUI[i].SetActive(true);
        }
        
        for(int j = currMatchCount; j < matchUI.Count; j++){
            matchUI[j].SetActive(false);
        }
    }

    public void useMatch()
    {
        if(CandleController.instance.candleInUse)
        {
            Dialogue.instance.addToDialogue("Should save my matches");
        }
        else
        {
            if(currMatchCount > 0 && CandleController.instance.currCandleCount > 0)
            {
                currMatchCount--;

                matchesFull = false;

                matchUI[currMatchCount].SetActive(false);
            }
            else{
                Debug.Log("out of matches or candles");
            }
        }
        
        
    }

    public void pickUpMatches()
    {
        randomCount = Random.Range(minMatchPickup, maxMatchPickup + 1);

        if(currMatchCount + randomCount > matchCap){
            for(int i = currMatchCount; i < matchUI.Count; i++){
                matchUI[i].SetActive(true);
            }
            currMatchCount = matchCap;
            matchesFull = true;
        }
        else{
            for(int i = currMatchCount; i < currMatchCount + randomCount; i++){
                matchUI[i].SetActive(true);
            }
            currMatchCount += randomCount;

            if(currMatchCount == matchCap)
            {
                matchesFull = true;
            }
        }
    }



}
