using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QTEController : MonoBehaviour
{
    [SerializeField] private List<GameObject> keys;
    [SerializeField] private int numberKeys;
    public GameObject[] qteObjects;
    public int[] qteKeys;
    private float horizontalInput, verticalInput;
    private Vector3 input;
    float time = 0f;
    public float timeDelay = 1.0f;
    public int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        qteObjects = new GameObject[numberKeys];
        qteKeys = new int[numberKeys+1];
        qteKeys[0] = 1;
        for (int i=0; i<numberKeys; i++)
        {
            int randomIndex = Random.Range(0, 4);
            qteObjects[i] = Instantiate(keys[randomIndex], this.transform);
            qteKeys[i+1] = randomIndex;
        }
        this.transform.DOLocalMoveX(-current * 85.0f, .5f);

    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(horizontalInput, verticalInput);
        if (time > 0f)
        {
            // Subtract the difference of the last time the method has been called
            time -= Time.deltaTime;
        }
        
        Move();


    }
    private void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(current == 0)
        {
            this.transform.DOLocalMoveX(-current * 85.0f, .5f);
            current++;
        }

        if (horizontalInput == 1 && time <= 0 && qteKeys[current]==3)
        {
            //D

            this.transform.DOLocalMoveX(-current *85.0f, .5f);
            time = timeDelay;
            current++;

        }
        if (horizontalInput == -1 && time <= 0 && qteKeys[current] == 1)
        {
            //A
            this.transform.DOLocalMoveX(-current * 85.0f, .5f);
            time = timeDelay;
            current++;
        }
        if (verticalInput == 1 && time <= 0 && qteKeys[current] == 0)
        {
            //W
            this.transform.DOLocalMoveX(-current * 85.0f, .5f);
            time = timeDelay;
            current++;
        }
        if (verticalInput == -1 && time <= 0 && qteKeys[current] == 2)
        {
            //W
            this.transform.DOLocalMoveX(-current * 85.0f, .5f);
            time = timeDelay;
            current++;

        }

    }
     
}
