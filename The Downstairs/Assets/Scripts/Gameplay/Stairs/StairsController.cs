using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    [SerializeField] private GameObject stairs;
    [SerializeField] private Vector3 moveTransform;

    public static StairsController instance {get; private set;}

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator moveStairs()
    {
        if(!CrawlingController.instance.goingDown)
        {
            moveTransform *= -1;
        }

        while(!CrawlingController.instance.crawlingFinished)
        {
            if(CrawlingController.instance.isCrawling)
            {
                stairs.transform.position += moveTransform;
            }

            yield return null;
        }
    }

}
