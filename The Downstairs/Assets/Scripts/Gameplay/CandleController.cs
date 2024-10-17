using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandleController : MonoBehaviour
{   
    [Header("Debug")]
    [SerializeField] public bool alwaysLit;

    [Header("Light Mechanics")]
    
    [Header("Light Features")]
    [SerializeField] private float lightingDuration;
    [SerializeField] private float glowDuration;
    [SerializeField] private float lightDuration;
    [SerializeField] private float lightInnerRadius;
    [SerializeField] private float lightOuterRadius;
    [SerializeField] private float lightIntensity;
    
    [Header("Flash Features")]
    [SerializeField] private float flashDuration;
    [SerializeField] private float flashRadius;
    [SerializeField] private float flashIntensity;

    [Header("General Lighting")]
    [SerializeField] public bool candleInUse;
    [SerializeField] private List<GameObject> candleLights;
    [SerializeField] private float maxIntensity;
    [SerializeField] private List<float> strikeChances;
    [SerializeField] private int strikeCount;

    [Header("Candle UI")]
    [SerializeField] public bool candlesFull;
    [SerializeField] public int candleCount;
    [SerializeField] private int candleCap;
    [SerializeField] private List<GameObject> candleUI = new();
    
    private float lightStart, secondsPassed;
    private Light2D currentLight;

    [Header("Candlelight Constraints")]

    [Header("West Pose")]
    [SerializeField] [Range(-120, -180)] public float botWest;
    [SerializeField] [Range(120, 180)] public float topWest;

    [Header("East Pose")]
    [SerializeField] [Range(-60, 0)] public float botEast;
    [SerializeField] [Range(0, 60)] public float topEast;

    [Header("South Pose")]
    [SerializeField] [Range(-120, -90)] public float leftSouth;
    [SerializeField] [Range(-90, -60)] public float rightSouth;

    [Header("North Pose")]
    [SerializeField] [Range(90, 120)] public float leftNorth;
    [SerializeField] [Range(60, 90)] public float rightNorth;

    public static CandleController instance {get; private set;}

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

        candleInUse = false;

        strikeCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FindActiveLight()
    {
        while(currentLight == null)
        {
            foreach(GameObject light in candleLights)
            {
                if(light.activeInHierarchy)
                {
                    currentLight = light.GetComponent<Light2D>();
                }
            }

            yield return null;
        }

        enableCandlelight(candleInUse); 
    }

    public void checkActiveLight()
    {
        foreach(GameObject light in candleLights)
        {
            if(light.activeInHierarchy)
            {
                currentLight = light.GetComponent<Light2D>();

                Debug.Log("currentlight: " + currentLight);

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
                    // Debug.Log("strikechances[strikecount]: " + strikeChances[strikeCount]);
                
                    useCandle();

                    StartCoroutine(Light(lightDuration, true));

                    strikeCount = 0;
                }
                else
                {
                    strikeCount++;

                    StartCoroutine(Light(flashDuration, false));
                }
            }
        }
        
    }

    private IEnumerator Light(float duration, bool glow)
    {
        enableCandlelight(true);

        currentLight.pointLightInnerRadius = lightInnerRadius;
        currentLight.pointLightOuterRadius = flashRadius;
        currentLight.intensity = flashIntensity;

        candleInUse = true;

        if(glow)
        {
            secondsPassed = 0;

            while(secondsPassed < lightingDuration)
            {
                if(GameManager.instance.gamePaused) yield return null;

                else
                {
                    secondsPassed += Time.deltaTime;

                    currentLight.intensity = Mathf.Lerp(flashIntensity, lightIntensity, secondsPassed/lightingDuration);

                    yield return null;
                }
            }

            secondsPassed = 0;

            while(secondsPassed < glowDuration)
            {
                if(GameManager.instance.gamePaused) yield return null;

                else
                {
                    secondsPassed += Time.deltaTime;

                    currentLight.pointLightOuterRadius = Mathf.Lerp(flashRadius, lightOuterRadius, secondsPassed/glowDuration);

                    yield return null;
                }
            }
        }

        secondsPassed = 0;

        while(secondsPassed < duration)
        {
            if(GameManager.instance.gamePaused) yield return null;

            else
            {
                secondsPassed += Time.deltaTime;

                yield return null;
            }
        }
        
        enableCandlelight(false);
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
        if(!alwaysLit) currentLight.intensity = enable ? maxIntensity : 0;
        
        else currentLight.intensity = maxIntensity;
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
