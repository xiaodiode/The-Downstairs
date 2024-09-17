using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Vector2 cameraXRange;
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool isTopdown;

    [SerializeField] private bool moveLocked;
    
    private float horizontalInput, verticalInput;
    private bool interactInput;
    private Vector3 velocity;
    private Vector3 newPosition;



    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); 

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

}
