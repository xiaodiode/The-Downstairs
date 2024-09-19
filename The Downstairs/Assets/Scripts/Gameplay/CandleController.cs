using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    public int candleCount, candleCap;
    [SerializeField] private List<GameObject> candleUI = new();

    // Start is called before the first frame update
    void Start()
    {
        initializeCandleUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initializeCandleUI(){
        for(int i=0; i < candleCount; i++){
            candleUI[i].SetActive(true);
        }
        
        for(int j=candleCount; j < candleUI.Count; j++){
            candleUI[j].SetActive(false);
        }
    }

    public void pickUpCandle(){
        if(candleCount + 1 <= candleCap){
            candleUI[candleCount].SetActive(true);
            candleCount++;
        }
    }
}
