using System;
using System.Diagnostics.SymbolStore;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class SidescrollPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float cameraBedroomY;

    [Header("Dependencies")]
    [SerializeField] private MatchController matchController;
    [SerializeField] private CandleController candleController;
    [SerializeField] private GameObject visualObject;
    private float horizontalInput, verticalInput;
    
    private Vector3 velocity;
    private Vector3 newPosition;
    private bool idle;
    
    
    enum Direction {Left, Right};
    private Direction currentDirection = Direction.Right;


    public static SidescrollPlayerController instance {get; private set;}

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
        velocity = Vector3.zero;

        idle = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move(){
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput == 0){
            idle = true;
        }
        SetDirection(horizontalInput);
        newPosition = playerRB.transform.position;
        newPosition.y = cameraBedroomY;
        

        velocity.x = horizontalInput* playerSpeed;
        playerRB.velocity = velocity;

        newPosition.z = playerCamera.transform.position.z;

        playerCamera.transform.position = newPosition;
    }

    private void OnInteract(){
        // Debug.Log("spacebar pressed");
    }

    private void OnUseMatch(){
        CandleController.instance.lightCandle();
    }

    private void SetDirection(float xaxis)
    {
        if (Mathf.Abs(xaxis) < 0.1) return;

        currentDirection = xaxis > 0 ? Direction.Left : Direction.Right;
        var scale = visualObject.transform.localScale;
        scale.z = Mathf.Sign(xaxis);
        visualObject.transform.localScale = scale;


    }

}
