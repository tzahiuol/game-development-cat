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
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float turnSpeed = 1;
    [SerializeField] float jumpForce = 1;

    Vector3 moveForce; 
    
    // Adjust these values for more cat-like movement
    [SerializeField] float acceleration = 5;
    [SerializeField] float maxMoveSpeed = 3;
    [SerializeField] float jumpMultiplier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        // ch = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {

        // Calculate the desired movement direction
        Vector3 desiredMoveDirection = transform.forward * movementVec.z + transform.right * movementVec.x;

        // Apply acceleration to the cat's movement
        moveForce = desiredMoveDirection * acceleration * Time.deltaTime;
        rb.AddForce(moveForce);

        // Limit the cat's maximum movement speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMoveSpeed);

        // Apply torque for turning
        if (movementVec.x != 0)
            rb.AddTorque(transform.up * movementVec.x * turnSpeed);

        // Play walk animation when moving
        // anim.SetBool("Walk", rb.velocity.magnitude > 0.1f);
        // }      
        
        shoveItem = false;
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
        rb.AddForce(transform.up * jumpForce * jumpMultiplier, ForceMode.Impulse);        
        onGround = false;
    }

    void OnShove(InputValue input){
        shoveItem = true;
    }

    void OnCollisionStay(Collision other){
        if(other.gameObject.tag == "shoveItem" && shoveItem == true){
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
        }
        
        onGround = true;
    }

    void OnCollisionExit(){
        onGround = false;
    }

    public void setDeath(bool aliveStat){
        anim.SetBool("Died", aliveStat);
    }
}
