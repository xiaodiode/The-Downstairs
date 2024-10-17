using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for working with UI components like Slider
using DG.Tweening;

public class QTEController : MonoBehaviour
{   
    [Header("QTE Mechanics")]
    [SerializeField] private int numberKeys;
    [SerializeField] private int[] qteKeys;
    [SerializeField] private float timeDelay = 1.0f;
    [SerializeField] private float qteTimeLimit = 3.0f; // Time limit for each QTE
    [Header("QTE Structure")]
    [SerializeField] private List<GameObject> keys;
    [SerializeField] private Transform qteSquare;
    [SerializeField] private Slider qteTimerSlider;  
    [SerializeField] private GameObject[] qteObjects;

    [Header("QTE Animation Settings")]
    [SerializeField] private float scaleAmtS = 0.8f;    
    [SerializeField] private float scaleAmtF = 0.8f;
    private int currentIndex = 0;
    
    private float qteTimer = 0f;

    private float time = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            currentIndex = 0;
        }
        qteObjects = new GameObject[numberKeys];
        qteKeys = new int[numberKeys];

        // Initialize qteObjects and qteKeys
        for (int i = 0; i < numberKeys; i++)
        {
            int randomIndex = Random.Range(0, 4);
            qteObjects[i] = Instantiate(keys[randomIndex], this.transform);
            qteKeys[i] = randomIndex;
        }

        qteTimer = qteTimeLimit;  
        qteTimerSlider.maxValue = qteTimeLimit;  
        qteTimerSlider.value = qteTimeLimit;  

        MoveToCurrentPosition(); 
    }

    // Update is called once per frame
    void Update()
    {
        // If all QTEs are completed, do nothing
        if (currentIndex >= numberKeys)
        {
            return;
        }

        // Update QTE timer
        qteTimer -= Time.deltaTime;
        qteTimerSlider.value = qteTimer;   
        if (qteTimer <= 0 && currentIndex < numberKeys)
        {
            Debug.Log("FAILED QTE");
            MoveToNextQTE(false); 
        }
 
        checkInput();
    }

    private void checkInput()
    {
        if ((Input.GetKeyDown(KeyCode.W) && qteKeys[currentIndex] == 0)
            || (Input.GetKeyDown(KeyCode.A) && qteKeys[currentIndex] == 1)
            || (Input.GetKeyDown(KeyCode.S) && qteKeys[currentIndex] == 2)
            || (Input.GetKeyDown(KeyCode.D) && qteKeys[currentIndex] == 3))
        {
            MoveToNextQTE(true);
        }

        // else MoveToNextQTE(false);
    }

    private void MoveToNextQTE(bool success)
    {
        if (success)
        {
            qteObjects[currentIndex].transform.DOScale(scaleAmtS, 0.25f);

        }
        else
        {
            qteObjects[currentIndex].transform.DOScale(-scaleAmtF, 0.25f);  
        }

        currentIndex++;

        if (currentIndex < numberKeys)
        {
            qteSquare.DOLocalMoveY(10f, 0.25f).From().SetEase(Ease.OutBack);
            this.transform.DOLocalMoveX(-currentIndex * 85.0f, .5f);
            qteTimer = qteTimeLimit;
            qteTimerSlider.value = qteTimeLimit; 
            time = timeDelay;
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