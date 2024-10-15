using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public int matchCount;
    [SerializeField] private int matchCap;
    [SerializeField] private int minMatchPickup, maxMatchPickup;
    [SerializeField] private List<GameObject> matchUI = new();
    [SerializeField] private CandleController candleController;

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
        for(int i=0; i < matchCount; i++){
            matchUI[i].SetActive(true);
        }
        
        for(int j=matchCount; j < matchUI.Count; j++){
            matchUI[j].SetActive(false);
        }
    }

    public void useMatch(){
        if(matchCount > 0 && candleController.candleCount > 0){
            matchCount--;
            matchUI[matchCount].SetActive(false);
        }
        Debug.Log("out of matches");
    }

    public void pickUpMatches(){
        randomCount = Random.Range(minMatchPickup, maxMatchPickup + 1);

        if(matchCount + randomCount > matchCap){
            for(int i = matchCount; i < matchUI.Count; i++){
                matchUI[i].SetActive(true);
            }
            matchCount = matchCap;
        }
        else{
            for(int i = matchCount; i < matchCount + randomCount; i++){
                matchUI[i].SetActive(true);
            }
            matchCount += randomCount;
        }
    }



}
