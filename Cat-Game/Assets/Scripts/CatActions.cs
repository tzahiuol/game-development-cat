using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatActions : MonoBehaviour
{
    Vector3 movementVec;
    Animator anim;
    Rigidbody rb;
    
    // CollisionHandler ch;
    bool shoveItem = false;
    bool onGround = false;
    bool walking = false;
    [SerializeField] float moveSpeed = 1000;
    [SerializeField] float turnSpeed = 100;
    [SerializeField] float jumpForce = 30;

    Vector3 moveForce; 
    
    // Adjust these values for more cat-like movement
    [SerializeField] float acceleration = 1000;
    [SerializeField] float maxMoveSpeed = 3;
    [SerializeField] float jumpMultiplier = 1.5f;

    //used for rotation lerping
    private Quaternion targetRotation;
    [SerializeField] float rotationAmount = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
        // ch = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired movement direction
        // Vector3 desiredMoveDirection = transform.forward * movementVec.z + transform.right * movementVec.x;
        Vector3 desiredMoveDirection = transform.forward * movementVec.z ;

        // Apply acceleration to the cat's movement
        moveForce = desiredMoveDirection * acceleration * Time.deltaTime;
        // moveForce = desiredMoveDirection * acceleration;
        
        rb.AddForce(moveForce);
        Vector3 rotation = new Vector3(0,movementVec.x,0) * turnSpeed * Time.deltaTime;
        transform.Rotate(rotation);


        // Limit the cat's maximum movement speed

        // Apply torque for turning
        // For rotation
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Update the target rotation 90 degrees to the left
            targetRotation *= Quaternion.Euler(0, -rotationAmount, 0); ;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Update the target rotation 90 degrees to the left
            targetRotation *= Quaternion.Euler(0, rotationAmount, 0);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        // Play walk animation when moving
        // anim.SetBool("Walk", rb.velocity.magnitude > 0.1f);
        // }  
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMoveSpeed);     
        
        
    }

    public void OnMove(InputValue input){

        Vector2 xyInput = input.Get<Vector2>(); 
        
        movementVec = new Vector3(xyInput.x, 0, xyInput.y);


        if (xyInput.magnitude > 0)
           anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);
   
    }

    public void OnJump(InputValue input){        
        
        if (onGround){            
            anim.SetTrigger("Jump");
        }

    }

    public void Jump(){
        // Apply a higher jump force to simulate cat-like jumping
        if (onGround == true)
        {
            rb.AddForce(transform.up * jumpForce * jumpMultiplier, ForceMode.Impulse);
            onGround = false;
        }
    }

    void OnShove(InputValue input){
        anim.SetTrigger("Shove");
    }

    void OnCollisionStay(Collision other){
        if(other.gameObject.tag == "shoveItem" && shoveItem == true){
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,20), ForceMode.Force);
        }
        
        onGround = true;
    }

    void OnCollisionExit(){
        onGround = false;
    }

    public void setDeath(bool aliveStat){
        anim.SetBool("Died", aliveStat);
    }


    private void shoveOn(){
        shoveItem = true;
    }
    private void shoveOff(){
        shoveItem = false;
    }
}
