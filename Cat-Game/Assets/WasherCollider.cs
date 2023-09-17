using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherCollider : MonoBehaviour
{
    public Animator washerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            washerAnimator.SetTrigger("WasherUp");
        }
    }
}
