using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEDebug : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Transform stairsCameraPosition;
    [SerializeField] public SceneController.ScenesType targetStairs;
    [SerializeField] private bool goingDown;

    // Start
    void Start() 
    {
        StartCoroutine(GameManager.instance.initializeGameStart());
    }

    // Start is called before the first frame update
    public void QTEDebugInit()
    {
        StartCoroutine(QTEDebugCoroutine());
    }

    IEnumerator QTEDebugCoroutine() 
    {
        SceneController scenecontroller = FindObjectOfType<SceneController>();
        while (scenecontroller.scenesDict == null)
        {
            yield return null;
        }
        Debug.Log("QTE Debug Initializion Starting");
        if(targetStairs == SceneController.ScenesType.Stairs1)
        {
            StartCoroutine(MetersController.instance.sanityMeter.decreaseMeter());
        }

        SceneController.instance.switchScenes(targetStairs);
        gameCamera.gameObject.transform.position = stairsCameraPosition.position;

        QTEController.instance.goingDown = goingDown;
        QTEController.instance.targetStairs = targetStairs;
        QTEController.instance.StartStairsGameplay();
    }
}
