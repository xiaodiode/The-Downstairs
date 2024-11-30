using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for working with UI components like Slider
using DG.Tweening;
using System;

[Serializable]
public struct KeyInput
{
    public string keyName;
    public KeyCode keyCode;
    public GameObject keyObject;
    public float      delay;
    public float      interval;
    public bool         failed;
}

public class QTEController : MonoBehaviour
{   
    public bool QTEfinished;
    public bool isTripping;
    public SceneController.ScenesType targetStairs;
    private Stairs currentStairs;
    private SceneController.ScenesType targetScene;
    public bool goingDown;
    public GameObject DKJAnimationMC;
    public GameObject ClickHighlighter;
    public GameObject KeyTarget;
    private DKJAnimator dkjanimator;

    [Header("QTE Mechanics")]
    [SerializeField] private float marginOfError;
    [SerializeField] private Color normalColor, hitColor, errorColor;
    [SerializeField] private Image sliderImage;
    [SerializeField] private int numberKeys;
    [SerializeField] private List<KeyInput> keyInputs = new();
    [SerializeField] private List<KeyInput> gabesKeyInputs = new();
    private float tripDelay;
    private float qteTimeLimit;
    
    [Header("QTE Structure")]
    [SerializeField] private GameObject qteUI;
    [SerializeField] private GameObject keyInputQueue;
    //[SerializeField] private List<GameObject> keys;
    [SerializeField] private Transform qteSquare;
    [SerializeField] private Slider qteTimerSlider;  

    [SerializeField] private GameObject startingQTE;
    [Header("QTE Animation Settings")]
    [SerializeField] public float scaleAmtS = 0.8f;    

    private List<KeyInput> keyQTEObjects;
    private int currentIndex;
    private bool firstKey;
    private float hitTime;

    public static QTEController instance {get; private set;}
    private bool sequenceCompleted = false;
    public float hitWindow = 0.3f;
    public float inputCooldownTime = 1f;
    public float hitVanishTime = 0.2f;
    public float missVanishTime = 0.4f;
    public float targetDistance = 5f;
    public float targetTime = 2f;
    public float qteInterval = 3f;
    public float missPenalty = 10;
    public float shakeMagnitude = .001f;
    public float randomTimeInterval = 0.1f;


    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }
        else{
            instance = this;
        }

    }

    void Start()
    {
        QTEfinished = true;
        enableUI(false);
        dkjanimator = DKJAnimationMC.GetComponent<DKJAnimator>();

    }

    // Update is called once per frame
    void Update()
    {

    }    
    private IEnumerator RunQTE()
    {
        StartCoroutine(SpawnQTE());
        StartCoroutine(CheckQTEDone());
        yield return null;
    }

    private IEnumerator SpawnQTE()
    {
        foreach (KeyInput key in keyQTEObjects)
        {
            SingleQTE sqte = key.keyObject.GetComponentInChildren<SingleQTE>();
            StartCoroutine(sqte.StartQTE());
            yield return new WaitForSeconds(key.interval);
        }
    }

    private IEnumerator CheckQTEDone()
    {
        bool done = false;
        while (!done) {
            done = true;
            foreach (KeyInput key in keyQTEObjects)
            {
                SingleQTE sqte = key.keyObject.GetComponentInChildren<SingleQTE>();
                done = done && sqte.isDone();
            }
            yield return null;
        }
        foreach (KeyInput key in keyQTEObjects)
        {
            GameObject keyobj = key.keyObject;
            Destroy(keyobj);
        }
        Debug.Log("QTE Completed");
        QTEfinished = true;
        yield return null;
        currentStairs =  SceneController.instance.stairsScenesDict[targetStairs]; 
        if (goingDown) {
            targetScene = currentStairs.botTargetScene;
        } else {
            targetScene = currentStairs.topTargetScene;
        }
        SceneController.instance.switchScenes(targetScene);
        Debug.Log("switching scenes to " + targetScene);
        GameManager.instance.setInteract(true, "- light match - [shift]");
        enableUI(false);
        yield return null;
    }

    
    private IEnumerator playQTE()
    {
        float timer = qteTimeLimit;
        QTEfinished = false;
        isTripping = true;
        startingQTE.SetActive(true);
        while(!Input.anyKeyDown)
        {
            yield return null;
        }
        Debug.Log("past first key");

        startingQTE.SetActive(false);

        StartCoroutine(CrawlingController.instance.StartCrawling());
        StartCoroutine(RunQTE());

        yield return null;

        firstKey = false;
    }


    public IEnumerator setupQTE()
    {
        CrawlingController.instance.goingDown = goingDown;
        if (goingDown) { 
            dkjanimator.ResetGoingDown(); 
        } else {
            dkjanimator.ResetGoingUp();
        }
        enableUI(false);
        currentIndex = 0;
        firstKey = true;
        
        keyQTEObjects = new();

        //float interval = startDelay;
        // creating new key combos
        for (int i = 0; i < numberKeys; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, keyInputs.Count);

            KeyInput newKeyQTE = keyInputs[randomIndex];
            newKeyQTE.keyObject = Instantiate(gabesKeyInputs[randomIndex].keyObject, KeyTarget.transform);
            newKeyQTE.keyObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            newKeyQTE.interval = UnityEngine.Random.Range(qteInterval-randomTimeInterval, qteInterval+randomTimeInterval);

            keyQTEObjects.Add(newKeyQTE);
        }

        yield return null;

        while(!CrawlingController.instance.ready) yield return null;
        
        tripDelay = CrawlingController.instance.getTripDelay(); 
        qteTimeLimit = CrawlingController.instance.getCrawlDelay();

        hitTime = marginOfError * qteTimeLimit;

        qteTimerSlider.maxValue = qteTimeLimit;  
        qteTimerSlider.value = qteTimeLimit;  

        StartCoroutine(playQTE()); // move this later
    }

    public void StartStairsGameplay()
    {
        foreach (Transform child in keyInputQueue.transform)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(setupQTE());
    }

    private void enableUI(bool enable)
    {
        qteUI.SetActive(enable);
    }

    public void resetQTE()
    {
        enableUI(false);
    }
}