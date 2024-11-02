using System.Collections;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    public bool stairsSwitched;
    public Stairs currentStairs;
    [SerializeField] private Vector3 moveTransform;
    [SerializeField] private SceneController.ScenesType targetScene;

    Vector3 newMoveTransform;

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
        stairsSwitched = false;

        newMoveTransform = moveTransform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator moveStairs()
    {
        stairsSwitched = false;

        if(!CrawlingController.instance.goingDown)
        {
            newMoveTransform = -moveTransform;
            targetScene = currentStairs.topTargetScene;
        }
        else
        {
            targetScene = currentStairs.botTargetScene;
            newMoveTransform = moveTransform;
        } 

        while(!CrawlingController.instance.crawlingFinished)
        {
            if(CrawlingController.instance.isCrawling)
            {
                currentStairs.transform.position += newMoveTransform;
            }

            yield return null;
        }
        
        SceneController.instance.switchScenes(targetScene);
        Debug.Log("switching scenes to " + targetScene);

        stairsSwitched = true;

        
    }

}
