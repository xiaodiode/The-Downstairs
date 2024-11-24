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
}

public class QTEController : MonoBehaviour
{   
    public bool QTEfinished;
    public bool isTripping;

    [Header("QTE Mechanics")]
    [SerializeField] private float marginOfError;
    [SerializeField] private Color normalColor, hitColor, errorColor;
    [SerializeField] private Image sliderImage;
    [SerializeField] private int numberKeys;
    [SerializeField] private List<KeyInput> keyInputs = new();
    private float tripDelay;
    private float qteTimeLimit;
    
    [Header("QTE Structure")]
    [SerializeField] private GameObject qteUI;
    [SerializeField] private GameObject keyInputQueue;
    [SerializeField] private List<GameObject> keys;
    [SerializeField] private Transform qteSquare;
    [SerializeField] private Slider qteTimerSlider;  

    [SerializeField] private GameObject startingQTE;
    [Header("QTE Animation Settings")]
    [SerializeField] private float scaleAmtS = 0.8f;    

    private List<KeyInput> keyQTEObjects;
    private int currentIndex;
    private bool firstKey;
    private float hitTime;

    public static QTEController instance {get; private set;}

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
    }

    // Update is called once per frame
    void Update()
    {

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

        yield return null;

        firstKey = false;

        while(currentIndex < numberKeys && !GameManager.instance.gameReset)
        {
            if(timer < 0)
            {
                TripLogic();

                yield return new WaitForSeconds(tripDelay);

                Debug.Log("finished tripping");

                timer = qteTimeLimit;
                changeSliderColor(normalColor);
            }

            else if(timer <= hitTime && timer >= 0)
            {
                changeSliderColor(hitColor);
                keyQTEObjects[currentIndex].keyObject.transform.DOScale(1.5f, 0.25f);
 
                if(Input.GetKeyDown(keyQTEObjects[currentIndex].keyCode))
                {
                    CrawlingController.instance.AddCrawl();
                    isTripping = false;

                    StartCoroutine(MoveToNextQTE());

                    timer = qteTimeLimit;
                    changeSliderColor(normalColor);
                }

                else if(Input.anyKeyDown && !firstKey)
                {
                    TripLogic();
                    
                    Debug.Log("tripping");

                    yield return new WaitForSeconds(tripDelay);

                    Debug.Log("finished tripping");

                    timer = qteTimeLimit;
                    changeSliderColor(normalColor);
                }
            }

            else if(Input.anyKeyDown && !firstKey)
            {
                TripLogic();

                Debug.Log("tripping");

                yield return new WaitForSeconds(tripDelay);

                Debug.Log("finished tripping");

                timer = qteTimeLimit;
                changeSliderColor(normalColor);
            }

            timer -= Time.deltaTime;
            qteTimerSlider.value = timer;

            yield return null;
        }
    }

    private void TripLogic()
    {   
        isTripping = true;

        MetersController.instance.sanityMeter.changeByAmount(-CrawlingController.instance.tripPenalty);
        CrawlingController.instance.AddTrip();
        changeSliderColor(errorColor);
        
        keyQTEObjects[currentIndex].keyObject.transform.DOScale(1f, 0.25f); 
        keyQTEObjects[currentIndex].keyObject.transform.DOShakePosition(tripDelay/3, new Vector3(7, 0, 0), randomness:90);
    }


    public IEnumerator setupQTE()
    {
        enableUI(true);

        currentIndex = 0;
        firstKey = true;
        
        keyQTEObjects = new();

        // creating new key combos
        for (int i = 0; i < numberKeys; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, keyInputs.Count);

            KeyInput newKeyQTE = keyInputs[randomIndex];
            newKeyQTE.keyObject = Instantiate(keyInputs[randomIndex].keyObject, keyInputQueue.transform);

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

    private IEnumerator MoveToNextQTE()
    {
        keyQTEObjects[currentIndex].keyObject.transform.DOScale(scaleAmtS, 0.25f);

        currentIndex++;

        if (currentIndex < numberKeys)
        {
            qteSquare.DOLocalMoveY(10f, 0.25f).From().SetEase(Ease.OutBack);
            keyInputQueue.transform.DOLocalMoveX(-currentIndex * 85.0f, .5f);
        }
        else
        {
            Debug.Log("QTE Completed");
            QTEfinished = true;

            while(!StairsController.instance.stairsSwitched) yield return null;
            
            StairsController.instance.stairsSwitched = false;

            enableUI(false);
            
        }
    }

    private void MoveToCurrentPosition()
    {
        keyInputQueue.transform.DOLocalMoveX(-currentIndex * 85.0f, 0.5f);
    }

    private void changeSliderColor(Color newColor)
    {
        sliderImage.color = newColor;
    }

    public void StartStairsGameplay()
    {
        foreach (Transform child in keyInputQueue.transform)
        {
            Destroy(child.gameObject);
        }

        StartCoroutine(setupQTE());
        MoveToCurrentPosition(); 
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