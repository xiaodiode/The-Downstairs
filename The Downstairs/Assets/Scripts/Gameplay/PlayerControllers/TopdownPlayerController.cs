using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class TopdownPlayerController : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Direction1 = Animator.StringToHash("Direction");
    [Header("Player Properties")]
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool isTopdown;

    [SerializeField] private bool moveLocked;
    [SerializeField] private float cameraBedroomY;

    private Vector3 velocity;
    private Vector3 newPosition;
    private float horizontalInput, verticalInput;
    private Vector3 input;

    // Candlelight Constraints
    private float botWest, topWest;
    private float botEast, topEast;
    private float leftSouth, rightSouth;
    private float leftNorth, rightNorth;
    private Vector3 mousePosition;
    private float angle, newAngle;
    private bool clampAngle;
    private bool idle = true;
    enum Direction {North, East, South, West};
    private Direction currentDirection = Direction.South;

    [SerializeField] private PlayerLightEclipse lightEclipse;
    [SerializeField] private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;

        if (isTopdown)
        {
            lightEclipse = GetComponent<PlayerLightEclipse>();
        }
        
        setLightConstraints();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(horizontalInput, verticalInput);

        if(!GameManager.instance.gamePaused) Move();

        else playerRB.velocity = Vector2.zero;

        if(isTopdown && lightEclipse.gameObject.activeSelf) 
        {
            updateCandleLight();
        }
        Animate();
        // Debug.Log("Dir" + currentDirection);
    }

    private void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        idle = horizontalInput == 0 && verticalInput == 0;

        if (horizontalInput == 0 ^ verticalInput == 0) 
        { //Updates Directional Enum taking into account the 
            SetDirection(horizontalInput,verticalInput);
        }        


        newPosition = playerRB.transform.position;

        velocity.x = horizontalInput * playerSpeed;
        velocity.y = verticalInput * playerSpeed;

        playerRB.velocity = velocity;

        newPosition.z = playerCamera.transform.position.z;

        playerCamera.transform.position = newPosition;
    }

    private void OnUseMatch()
    {
        CandleController.instance.lightCandle();
    }

    private void updateCandleLight(){
        if (isTopdown)
        {
            mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

            switch(currentDirection){
                case Direction.North:
                    // if(angle <= leftNorth && angle >= rightNorth)
                    // {
                    //     newAngle = angle;
                    //     clampAngle = false;
                    // }
                    // else if(!clampAngle)
                    // {
                    //     if(angle < rightNorth && angle >= -90) newAngle = rightNorth;
                    
                    //     else if((angle > leftNorth && angle <= 180) || (angle >= -180 && angle < 90)) newAngle = leftNorth;

                    //     clampAngle = true;
                    // }
                    newAngle = 90;

                    break;
                case Direction.South:
                    // if(angle <= rightSouth && angle >= leftSouth)
                    // {
                    //     newAngle = angle;
                    //     clampAngle = false;
                    // }
                    // else if(!clampAngle)
                    // {
                    //     if(angle > rightSouth && angle <= 90) newAngle = rightSouth;
                    
                    //     else if((angle < leftSouth && angle >= -180) || (angle <= 180 && angle > 90)) newAngle = leftSouth;

                    //     clampAngle = true;
                    // }
                    newAngle = -90;

                    break;

                case Direction.East:
                    // if(angle <= topEast && angle >= botEast)
                    // {
                    //     newAngle = angle;
                    //     clampAngle = false;
                    // }
                    // else if(!clampAngle)
                    // {
                    //     if(angle > topEast && angle <= 180) newAngle = topEast;
                    
                    //     else if(angle < botEast && angle >= -180) newAngle = botEast;

                    //     clampAngle = true;
                    // }
                    newAngle = 0;

                    break;

                case Direction.West:
                    // if((angle <= 180 && angle >= topWest) || (angle >= -180 && angle <= botWest))
                    // {
                    //     newAngle = angle;
                    //     clampAngle = false;
                    // }
                    // else if(!clampAngle)
                    // {
                    //     if(angle < topWest && angle >= 0) newAngle = topWest;
                    
                    //     else if(angle > botWest && angle < 0) newAngle = botWest;

                    //     clampAngle = true;
                    // }
                    newAngle = -180;

                    break;
                
            }

            lightEclipse.angle = newAngle * Mathf.Deg2Rad;
        }
    }

    private void setLightConstraints()
    {
        clampAngle = false;

        botWest = CandleController.instance.botWest;
        topWest = CandleController.instance.topWest;
        botEast = CandleController.instance.botEast;
        topEast = CandleController.instance.topEast;
        leftSouth = CandleController.instance.leftSouth;
        rightSouth = CandleController.instance.rightSouth;
        leftNorth = CandleController.instance.leftNorth;
        rightNorth = CandleController.instance.rightNorth;
    }

    private void SetDirection(float xaxis, float yaxis)
    {
        if (xaxis > 0) {
            currentDirection = Direction.East;
        } else if (xaxis < 0) {
            currentDirection = Direction.West;
        } else if (yaxis > 0) {
            currentDirection = Direction.North;
        } else if (yaxis < 0) {
            currentDirection = Direction.South;
        }
    }
    private void Animate()
    {
        animator.SetFloat(Horizontal, horizontalInput);
        animator.SetFloat(Vertical, verticalInput);
        animator.SetBool(Idle, idle);
        animator.SetInteger(Direction1, (int)currentDirection);
        //Debug.Log("animating");
    }


}
