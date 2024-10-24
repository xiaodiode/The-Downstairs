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
    [Header("QTE Mechanics")]
    [SerializeField] private int numberKeys;
    [SerializeField] private List<KeyInput> keyInputs = new();
    private float tripDelay;
    private float qteTimeLimit;
    
    [Header("QTE Structure")]
    [SerializeField] private List<GameObject> keys;
    [SerializeField] private Transform qteSquare;
    [SerializeField] private Slider qteTimerSlider;  

    [Header("QTE Animation Settings")]
    [SerializeField] private float scaleAmtS = 0.8f;    

    private List<KeyInput> keyQTEObjects;
    private int currentIndex;
    private bool firstKey;

    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        StartCoroutine(resetQTE());
        
        MoveToCurrentPosition(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator playQTE()
    {
        float timer = qteTimeLimit;

        while(!Input.anyKeyDown)
        {
            yield return null;
        }
        Debug.Log("past first key");

        CrawlingController.instance.AddCrawl();
        StartCoroutine(CrawlingController.instance.StartCrawling());

        yield return null;

        firstKey = false;

        while(currentIndex < numberKeys)
        {
            if(timer <= 0)
            {
                CrawlingController.instance.AddTrip();
                Debug.Log("tripping");

                yield return new WaitForSeconds(tripDelay);

                Debug.Log("finished tripping");

                // MoveToNextQTE();
                timer = qteTimeLimit;
            }

            else if(Input.GetKeyDown(keyQTEObjects[currentIndex].keyCode))
            {
                CrawlingController.instance.AddCrawl();
                MoveToNextQTE();
                timer = qteTimeLimit;
            }

            else if(Input.anyKeyDown && !firstKey)
            {
                CrawlingController.instance.AddTrip();

                Debug.Log("tripping");

                yield return new WaitForSeconds(tripDelay);

                Debug.Log("finished tripping");

                // MoveToNextQTE();
                timer = qteTimeLimit;
            }

            timer -= Time.deltaTime;
            qteTimerSlider.value = timer;

            yield return null;
        }
    }

    private IEnumerator resetQTE()
    {
        currentIndex = 0;
        firstKey = true;
        keyQTEObjects = new();

        while(!CrawlingController.instance.ready) yield return null;
        
        tripDelay = CrawlingController.instance.getTripDelay(); 
        qteTimeLimit = CrawlingController.instance.crawlTime;

        // creating new key combos
        for (int i = 0; i < numberKeys; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, keyInputs.Count);

            KeyInput newKeyQTE = keyInputs[randomIndex];
            newKeyQTE.keyObject = Instantiate(keyInputs[randomIndex].keyObject, this.transform);

            keyQTEObjects.Add(newKeyQTE);
        }
        
        qteTimerSlider.maxValue = qteTimeLimit;  
        qteTimerSlider.value = qteTimeLimit;  

        StartCoroutine(playQTE()); // move this later
    }

    private void MoveToNextQTE()
    {
        keyQTEObjects[currentIndex].keyObject.transform.DOScale(scaleAmtS, 0.25f);

        currentIndex++;

        if (currentIndex < numberKeys)
        {
            qteSquare.DOLocalMoveY(10f, 0.25f).From().SetEase(Ease.OutBack);
            this.transform.DOLocalMoveX(-currentIndex * 85.0f, .5f);
        }
        else
        {
            Debug.Log("QTE Completed");
            this.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void MoveToCurrentPosition()
    {
        this.transform.DOLocalMoveX(-currentIndex * 85.0f, 0.5f);
    }
}