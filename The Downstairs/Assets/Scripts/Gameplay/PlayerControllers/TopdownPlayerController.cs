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

    private float horizontalInput, verticalInput;
    
    private Vector3 velocity;
    private Vector3 newPosition;
    
    private Vector3 mousePosition;
    private float angle, newAngle;
    private bool hitMinAngle, hitMaxAngle, clampAngle;
    private float oldAngle;
    
    // private bool idle;

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

        clampAngle = false;
        // idle = true;
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(horizontalInput, verticalInput);

        Move();

        if(lightEclipse.gameObject.activeSelf) 
        {
            updateCandleLight();
        }

        // Debug.Log("Dir" + currentDirection);
    }
    private void Move(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // if(horizontalInput == 0 && verticalInput == 0){
        //     idle = true;
        // }
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
        CandleController.instance.lightCandle();
    }

    private void updateCandleLight(){
        if (isTopdown)
        {
            mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

            switch(currentDirection){
                case Direction.North:
                    if(angle <= 120 && angle >= 60)
                    {
                        newAngle = angle;
                        clampAngle = false;
                    }
                    else if(!clampAngle)
                    {
                        if(angle < 60 && angle >= -90) newAngle = 60;
                    
                        else if((angle > 120 && angle <= 180) || (angle >= -180 && angle < 90)) newAngle = 120;

                        clampAngle = true;
                    }

                    break;
                case Direction.South:
                    if(angle <= -60 && angle >= -120)
                    {
                        newAngle = angle;
                        clampAngle = false;
                    }
                    else if(!clampAngle)
                    {
                        if(angle > -60 && angle <= 90) newAngle = -60;
                    
                        else if((angle < -120 && angle >= -180) || (angle <= 180 && angle > 90)) newAngle = -120;

                        clampAngle = true;
                    }

                    break;

                case Direction.East:
                    if(angle <= 60 && angle >= -60)
                    {
                        newAngle = angle;
                        clampAngle = false;
                    }
                    else if(!clampAngle)
                    {
                        if(angle > 60 && angle <= 180) newAngle = 60;
                    
                        else if(angle < -60 && angle >= -180) newAngle = -60;

                        clampAngle = true;
                    }
                    break;

                case Direction.West:
                    if((angle <= 180 && angle >= 120) || (angle >= -180 && angle <= -120))
                    {
                        newAngle = angle;
                        clampAngle = false;
                    }
                    else if(!clampAngle)
                    {
                        if(angle < 120 && angle >= 0) newAngle = 120;
                    
                        else if(angle > -120 && angle < 0) newAngle = -120;

                        clampAngle = true;
                    }
                    break;
                
            }

            // Debug.Log("angle of mouse: " + angle);

            lightEclipse.angle = newAngle * Mathf.Deg2Rad;
        }
    }

    private float clampLightBounds(float currAngle, float minAngle, float maxAngle, bool checkMinFirst)
    {
        if(currAngle < minAngle && oldAngle >= minAngle)
        {
            hitMinAngle = true;
        } 
        else if(currAngle > maxAngle && oldAngle <= maxAngle)
        {
            hitMaxAngle = true;
        }
        else
        {
            hitMinAngle = false;
            hitMaxAngle = false;
        }

        if(checkMinFirst)
        {
            if(hitMinAngle) currAngle = minAngle;
            else if(hitMaxAngle) currAngle = maxAngle;
        }
        else
        {
            if(hitMaxAngle) currAngle = maxAngle;
            else if(hitMinAngle) currAngle = minAngle;
        }
        

        oldAngle = currAngle;

        return currAngle;
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
