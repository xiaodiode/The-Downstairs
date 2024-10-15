using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public int matchCount;
    public bool matchesFull;
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
        intializeMatchUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void intializeMatchUI(){
        matchesFull = false;

        for(int i=0; i < matchCount; i++){
            matchUI[i].SetActive(true);
        }
        
        for(int j=matchCount; j < matchUI.Count; j++){
            matchUI[j].SetActive(false);
        }
    }

    public void useMatch(){
        if(CandleController.instance.candleInUse)
        {
            Dialogue.instance.addToDialogue("Should save my matches");
        }
        else
        {
            if(matchCount > 0 && CandleController.instance.candleCount > 0)
            {
                matchCount--;

                matchesFull = false;

                matchUI[matchCount].SetActive(false);
            }
            else{
                Debug.Log("out of matches or candles");
            }
        }
        
        
    }

    public void pickUpMatches(){
        randomCount = Random.Range(minMatchPickup, maxMatchPickup + 1);

        if(matchCount + randomCount > matchCap){
            for(int i = matchCount; i < matchUI.Count; i++){
                matchUI[i].SetActive(true);
            }
            matchCount = matchCap;
            matchesFull = true;
        }
        else{
            for(int i = matchCount; i < matchCount + randomCount; i++){
                matchUI[i].SetActive(true);
            }
            matchCount += randomCount;

            if(matchCount == matchCap)
            {
                matchesFull = true;
            }
        }
    }



}
