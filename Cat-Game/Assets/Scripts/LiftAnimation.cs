using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftAnimation : MonoBehaviour
{
    [SerializeField] 
    private Animator liftAnimation;

    //sets a bool to tell the scrip whether the lifts needs to go down or up for the first time
    public bool liftInitialUp;

    // Start is called before the first frame update
    void Start()
    {
        liftAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
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
    }
}
