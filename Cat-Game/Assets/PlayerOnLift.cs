using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnLift : MonoBehaviour
{

    public Rigidbody catRb;
    public GameObject cat;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //https://docs.unity3d.com/ScriptReference/Transform.SetParent.html
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
