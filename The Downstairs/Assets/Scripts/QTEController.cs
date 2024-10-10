using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for working with UI components like Slider
using DG.Tweening;

public class QTEController : MonoBehaviour
{
    [SerializeField] private List<GameObject> keys;
    [SerializeField] private int numberKeys;
    [SerializeField] private Slider qteTimerSlider; // Reference to the UI Slider

    public GameObject[] qteObjects;
    public int[] qteKeys;
    private float horizontalInput, verticalInput;
    private Vector3 input;
    float time = 0f;
    public float timeDelay = 1.0f;
    public int current = 0;
    public float qteTimeLimit = 3.0f; // Time limit for each QTE
    private float qteTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        qteObjects = new GameObject[numberKeys];
        qteKeys = new int[numberKeys];

        // Initialize qteObjects and qteKeys
        for (int i = 0; i < numberKeys; i++)
        {
            int randomIndex = Random.Range(0, 4);
            qteObjects[i] = Instantiate(keys[randomIndex], this.transform);
            qteKeys[i] = randomIndex;
        }

        qteTimer = qteTimeLimit; // Initialize QTE timer
        qteTimerSlider.maxValue = qteTimeLimit; // Set slider max value to the QTE time limit
        qteTimerSlider.value = qteTimeLimit; // Initialize slider to full

        MoveToCurrentPosition(); // Start at the initial position
    }

    // Update is called once per frame
    void Update()
    {
        // If all QTEs are completed, do nothing
        if (current >= numberKeys)
        {
            return;
        }

        // Gather player input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Update QTE timer
        qteTimer -= Time.deltaTime;
        qteTimerSlider.value = qteTimer; // Update slider value to reflect remaining time

        // Check if time limit for the QTE is up
        if (qteTimer <= 0 && current < numberKeys)
        {
            Debug.Log("FAILED QTE");
            MoveToNextQTE(); // Move to the next one on failure
        }

        // Process player movement input
        Move();
    }

    private void Move()
    {
        if (time > 0f)
        {
            // Cooldown for movement
            time -= Time.deltaTime;
            return;
        }

        if (horizontalInput == 1 && qteKeys[current] == 3)
        {
            // D key
            MoveToNextQTE();
        }
        else if (horizontalInput == -1 && qteKeys[current] == 1)
        {
            // A key
            MoveToNextQTE();
        }
        else if (verticalInput == 1 && qteKeys[current] == 0)
        {
            // W key
            MoveToNextQTE();
        }
        else if (verticalInput == -1 && qteKeys[current] == 2)
        {
            // S key
            MoveToNextQTE();
        }
    }

    private void MoveToNextQTE()
    {
        current++;

        if (current < numberKeys)
        {
            this.transform.DOLocalMoveX(-current * 85.0f, .5f);
            qteTimer = qteTimeLimit; // Reset QTE timer for the next QTE
            qteTimerSlider.value = qteTimeLimit; // Reset slider to full
            time = timeDelay; // Set delay for next input
        }
        else
        {
            Debug.Log("QTE Completed"); // Log when QTE sequence is done
        }
    }

    private void MoveToCurrentPosition()
    {
        this.transform.DOLocalMoveX(-current * 85.0f, 0.5f);
    }
}