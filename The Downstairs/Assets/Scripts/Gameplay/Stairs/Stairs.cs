using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [Header("Target Scenes")]
    [SerializeField] public SceneController.ScenesType topTargetScene;
    [SerializeField] public SceneController.ScenesType botTargetScene;

    [Header("Stairs Structure")]
    [SerializeField] public bool reversed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
