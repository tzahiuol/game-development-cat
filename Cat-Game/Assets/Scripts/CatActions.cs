using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatActions : MonoBehaviour
{

    Vector3 movementVec;
    Animator anim;
    Rigidbody rb;
    ColliosionHandler ch;
    bool shoveItem = false;
    bool onGround = false;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float turnSpeed = 0.1f;
    [SerializeField] float jumpForce = 3f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        ch = GetComponent<ColliosionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        rb.AddForce(transform.forward * movementVec.z * moveSpeed);
        if(onGround){
            rb.AddTorque(transform.up * movementVec.x * turnSpeed);
        }
        

        shoveItem = false;
        
    }

    public void OnMove(InputValue input){
        Vector2 xyInput = input.Get<Vector2>();
        movementVec = new Vector3(xyInput.x, 0, xyInput.y);

        if(!anim.GetBool("Walk")){
            anim.SetBool("Walk", true);            
        }
        else{
            anim.SetBool("Walk", false);
        }
    }

    public void OnJump(InputValue input){
        anim.SetTrigger("Jump");
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        onGround = false;
    }

    void OnShove(InputValue input){
        shoveItem = true;
        
    }

    void OnCollisionStay(Collision other){
        // Debug.Log(onGround);
        if(other.gameObject.tag == "shoveItem" && shoveItem == true){
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
            
        }
        onGround = true;
    }

    void OnCollisionExit(){
        onGround = false;
    }
    



}
