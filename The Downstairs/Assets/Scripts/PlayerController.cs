using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Vector2 cameraXRange;
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool isTopdown;

    [SerializeField] private bool moveLocked;
    
    private float horizontalInput, verticalInput;
    public bool interactInput;
    private Vector3 velocity;
    private Vector3 newPosition;


    // Start is called before the first frame update
    void Start()
    {
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

        if(isTopdown){
            velocity.y = (float)Math.Round(verticalInput)*playerSpeed;
        }
        velocity.x = (float)Math.Round(horizontalInput)*playerSpeed;

        playerRB.velocity = velocity;

        newPosition = playerRB.transform.position;
        newPosition.y = playerCamera.transform.position.y;
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
        Debug.Log("spacebar pressed");

        interactInput = true;
    }

}
