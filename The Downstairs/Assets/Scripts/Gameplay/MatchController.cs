using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField] private List<GameObject> matchUI = new();
    [SerializeField] private CandleController candleController;

    public int matchCount, matchCap;

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

    }



}
