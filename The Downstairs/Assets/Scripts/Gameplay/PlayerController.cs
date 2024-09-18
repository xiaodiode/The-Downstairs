using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool inBedroom;
    [SerializeField] private bool isTopdown;

    [SerializeField] private bool moveLocked;
    
    private float horizontalInput, verticalInput;
    private float cameraBedroomY;
    private Vector3 velocity;
    private Vector3 newPosition;


    // Start is called before the first frame update
    void Start()
    {
        if(inBedroom){
            cameraBedroomY = playerCamera.transform.position.y;
        }
        
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveLocked) {
            Move();
        }
    }
    private void Move(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        newPosition = playerRB.transform.position;

        if(inBedroom){
            newPosition.y = cameraBedroomY;
        }
        else if(isTopdown){
            velocity.y = (float)Math.Round(verticalInput)*playerSpeed;
        }

        velocity.x = (float)Math.Round(horizontalInput)*playerSpeed;

        playerRB.velocity = velocity;

        newPosition.z = playerCamera.transform.position.z;

        playerCamera.transform.position = newPosition;
    }

    public void SetInteract(){
        moveLocked = true;
    }

    public void UnsetInteract(){
        moveLocked = false;
    }

    private void OnInteract(){
        // Debug.Log("spacebar pressed");

    }

}
