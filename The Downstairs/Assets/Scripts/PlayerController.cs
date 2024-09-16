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
    
    private float horizontalInput, verticalInput;
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
        move();
    }

    private void move(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(!isTopdown){
            velocity.x = (float)Math.Round(horizontalInput)*playerSpeed;
        }
        
        else if(isTopdown){
            velocity.y = (float)Math.Round(verticalInput)*playerSpeed;
        }

        playerRB.velocity = velocity;

        newPosition = playerRB.transform.position;
        newPosition.y = playerCamera.transform.position.y;
        newPosition.z = playerCamera.transform.position.z;

        playerCamera.transform.position = newPosition;
    }


}
