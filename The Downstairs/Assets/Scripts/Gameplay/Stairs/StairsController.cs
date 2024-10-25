using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    public Stairs currentStairs;
    [SerializeField] private Vector3 moveTransform;
    [SerializeField] private SceneController.ScenesType targetScene;

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
            targetScene = currentStairs.topTargetScene;
        }
        else targetScene = currentStairs.botTargetScene;

        while(!CrawlingController.instance.crawlingFinished)
        {
            if(CrawlingController.instance.isCrawling)
            {
                currentStairs.transform.position += moveTransform;
            }

            yield return null;
        }
        
        SceneController.instance.switchScenes(targetScene);
        Debug.Log("switching scenes to " + targetScene);

        
    }

}
