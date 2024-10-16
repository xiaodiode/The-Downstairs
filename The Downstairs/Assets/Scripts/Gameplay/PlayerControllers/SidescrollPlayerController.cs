using System;
using System.Diagnostics.SymbolStore;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class SidescrollPlayerController : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float cameraBedroomY;

    [SerializeField] private Animator animator;

    [Header("Dependencies")]
    [SerializeField] private MatchController matchController;
    [SerializeField] private CandleController candleController;
    [SerializeField] private GameObject visualObject;
    private float horizontalInput;
    [SerializeField] private SpriteRenderer sprite;

    
    private Vector3 velocity;
    private Vector3 newPosition;
    
    
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
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Debug.Log(currentDirection.ToString());
    }
    private void Move(){
        horizontalInput = Input.GetAxisRaw("Horizontal");

        idle = horizontalInput == 0;
        sprite.flipX = currentDirection == Direction.Left ? true : false;
        SetDirection(horizontalInput);
        newPosition = playerRB.transform.position;
        newPosition.y = cameraBedroomY;
        

        velocity.x = horizontalInput* playerSpeed;
        playerRB.velocity = velocity;

        newPosition.z = playerCamera.transform.position.z;

        playerCamera.transform.position = newPosition;
        animator.SetBool(IsMoving, !idle);
    }

    private void OnInteract(){
        // Debug.Log("spacebar pressed");
    }

    private void OnUseMatch(){
        CandleController.instance.lightCandle();
    }

    private void SetDirection(float xaxis)
    {
        currentDirection = xaxis switch
        {
            < 0 => Direction.Left,
            > 0 => Direction.Right,
            _ => currentDirection
        };
        var scale = visualObject.transform.localScale;
        scale.z = Mathf.Sign(xaxis);
        visualObject.transform.localScale = scale;


    }

}
