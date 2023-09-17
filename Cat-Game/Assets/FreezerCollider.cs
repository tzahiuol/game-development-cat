using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerCollider : MonoBehaviour
{

    //https://docs.unity3d.com/ScriptReference/Animator.html
    public Animator fridgeAnimator;

    //sets a bool to tell the scrip whether the lifts needs to go down or up for the first time
    public bool liftInitialUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter.html
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fridgeAnimator.SetTrigger("getIcy");
        }
    }
}
