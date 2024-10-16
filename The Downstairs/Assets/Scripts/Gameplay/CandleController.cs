using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandleController : MonoBehaviour
{
    public int candleCount;
    public bool candlesFull, candleInUse;

    [Header("Light Mechanics")]
    [SerializeField] private List<GameObject> candleLights;
    [SerializeField] private List<float> strikeChances;
    [SerializeField] private float lightDuration;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float flashDuration;
    [SerializeField] private int strikeCount;
    private float lightStart, secondsPassed;

    [Header("Candle UI")]
    [SerializeField] private int candleCap;
    [SerializeField] private List<GameObject> candleUI = new();

    private Light2D currentLight;

    public static CandleController instance {get; private set;}

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
        initializeCandleUI();

        enableCandlelight(true);

        candleInUse = false;

        strikeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // checkActiveLight();
    }

    public void checkActiveLight()
    {
        foreach(GameObject light in candleLights)
        {
            if(light.activeInHierarchy)
            {
                currentLight = light.GetComponent<Light2D>();

                enableCandlelight(candleInUse);
            }
        }
    }

    public void lightCandle()
    {
        float randomChance = Random.Range(0.0f, 1.0f);
        Debug.Log("randomChance: " + randomChance);

        if(candleCount > 0 && MatchController.instance.matchCount > 0)
        {
            if(candleInUse)
            {
                Dialogue.instance.addToDialogue("Should wait till this candle goes out");
            }
            else
            {
                MatchController.instance.useMatch();

                if(randomChance <= strikeChances[strikeCount])
                {
                    Debug.Log("strikechances[strikecount]: " + strikeChances[strikeCount]);
                    lightStart = Time.time;

                    useCandle();

                    StartCoroutine(Light(lightDuration));

                    strikeCount = 0;

                    Debug.Log("candle is lit");
                }
                else
                {
                    strikeCount++;

                    StartCoroutine(Light(flashDuration));
                }
            }
        }
        
    }

    private IEnumerator Light(float duration)
    {
        secondsPassed = 0;

        candleInUse = true;

        while(secondsPassed < duration)
        {
            secondsPassed = Time.time - lightStart;

            yield return null;
        }

        candleInUse = false;
    }


    private void useCandle()
    {
        candleCount--;

        candlesFull = false;

        candleUI[candleCount].SetActive(false);
    }

    private void enableCandlelight(bool enable)
    {
        currentLight.intensity = enable ? maxIntensity : 0;
    }

    private void initializeCandleUI(){
        candlesFull = false;

        for(int i=0; i < candleCount; i++){
            candleUI[i].SetActive(true);
        }
        
        for(int j = candleCount; j < candleUI.Count; j++){
            candleUI[j].SetActive(false);
        }
    }

    public void pickUpCandle(){
        if(!candlesFull){
            candleUI[candleCount].SetActive(true);
            candleCount++;

            if(candleCount == candleCap)
            {
                candlesFull = true;
            }
        }
    }
}
