using System.Collections;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    public bool stairsSwitched;
    [SerializeField] private Vector3 moveTransform;

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

    //public IEnumerator moveStairs()
    //{
    //    stairsSwitched = false;

    //    
    //    //SceneController.instance.switchScenes(targetScene);
    //    // ^Whoever thought that control major scene switches in the stair animator script needs to be shot

    //    
    //}

}
