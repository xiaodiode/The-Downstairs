using System;
using System.Diagnostics.SymbolStore;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class TopdownPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool isTopdown;

    [SerializeField] private bool moveLocked;
    [SerializeField] private float cameraBedroomY;

    [Header("Dependencies")]
    [SerializeField] private MatchController matchController;
    [SerializeField] private CandleController candleController;
    private float horizontalInput, verticalInput;
    
    private Vector3 velocity;
    private Vector3 newPosition;
    
    
    
    private bool idle;

    private Vector3 input;
    
    
    enum Direction {North, East, South, West};
    private Direction currentDirection = Direction.South;

    [SerializeField] private PlayerLightEclipse lightEclipse;


    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;

        if (isTopdown)
        {
            lightEclipse = GetComponent<PlayerLightEclipse>();
        }
        idle = true;
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(horizontalInput, verticalInput);

        Move();

        if (isTopdown)
        {
            var mousePos = playerCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var angle = Mathf.Atan2(mousePos.y, mousePos.x);

            switch(currentDirection){
                case Direction.South:
                    angle = Mathf.Clamp(angle, -1.8f, -0.8f);
                    break;
                case Direction.West:
                    angle = Mathf.Clamp(angle, 1.8f, 2.8f);
                    break;
                case Direction.North:
                    angle = Mathf.Clamp(angle, 0.8f, 1.8f);
                    break;
                case Direction.East:
                    angle = Mathf.Clamp(angle,-0.8f, 0.8f);
                    break;
            }

            Debug.Log("angle of mouse: " + angle);

            lightEclipse.angle = angle;
        }

        // Debug.Log("Dir" + currentDirection);
    }
    private void Move(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(horizontalInput == 0 && verticalInput == 0){
            idle = true;
        }
        if (horizontalInput == 0 ^ verticalInput == 0) { //Updates Directional Enum taking into account the 
            SetDirection(horizontalInput,verticalInput);
        }        


        newPosition = playerRB.transform.position;

        velocity.x = horizontalInput * playerSpeed;
        velocity.y = verticalInput * playerSpeed;

        playerRB.velocity = velocity;

        newPosition.z = playerCamera.transform.position.z;

        playerCamera.transform.position = newPosition;
    }


    private void OnInteract(){
        // Debug.Log("spacebar pressed");
    }

    private void OnUseMatch(){
        matchController.useMatch();
    }

    private void SetDirection(float xaxis, float yaxis){
        if (xaxis > 0) {
            currentDirection = Direction.East;
        } else if (xaxis < 0) {
            currentDirection = Direction.West;
        } else if (yaxis > 0) {
            currentDirection = Direction.North;
        } else {
            currentDirection = Direction.South;
        }
    }

}
