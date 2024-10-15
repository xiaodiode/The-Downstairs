using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RendererController : MonoBehaviour
{
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private GameObject screensCamera;
    private UniversalAdditionalCameraData gameURP, screensURP;

    public Dictionary<RendererType, int> rendererEffect;

    public enum RendererType
    {
        Light2D,
        VHS
    }

    public static RendererController instance {get; private set;}
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
        rendererEffect = new()
        {
            {RendererType.Light2D, 0},
            {RendererType.VHS, 1}
        };

        gameURP = gameCamera.GetComponent<UniversalAdditionalCameraData>();
        screensURP = screensCamera.GetComponent<UniversalAdditionalCameraData>();

        toggleScreensRenderer(RendererType.VHS);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleScreensRenderer(RendererType type)
    {
        screensURP.SetRenderer(rendererEffect[type]);
    }

    public void toggleGameRenderer(RendererType type)
    {
        gameURP.SetRenderer(rendererEffect[type]);
    }


}
