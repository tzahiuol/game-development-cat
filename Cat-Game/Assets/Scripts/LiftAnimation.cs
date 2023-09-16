using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftAnimation : MonoBehaviour
{
    [SerializeField] 
    private Animator liftAnimation;

    //sets a bool to tell the scrip whether the lifts needs to go down or up for the first time
    public bool liftInitialUp;

    //ger rigid body of player to freeze it while the animation is ongoing
    private RigidbodyConstraints originalConstraints;
    private Rigidbody catRb;
    //pivot to transform position
    public GameObject pivot;
    [SerializeField]
    private float lerpSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        liftAnimation = GetComponent<Animator>();

        //initalise the rigid body
        catRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        originalConstraints = catRb.constraints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            if (liftInitialUp)
            {
                liftAnimation.SetTrigger("LiftUp");
                liftInitialUp = false;
            }
            else
            {
                liftAnimation.SetTrigger("LiftDown");
                liftInitialUp = true;
            }

            //make cat move into the middle of the lift
            other.gameObject.transform.position = Vector3.Lerp(transform.position, pivot.transform.position, lerpSpeed);
            catRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ; // Freeze rigidbody
            // unfreeze rb after 3 seconds
            StartCoroutine(UnfreezeRigidbodyAfterDelay(3.0f));
        }
    }

    //unfreezing rb
    IEnumerator UnfreezeRigidbodyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        catRb.constraints = originalConstraints; 
    }
}
