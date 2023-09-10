using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ColliosionHandler : MonoBehaviour
{
    //count the cats lives
    [SerializeField]
    private int lives = 3;

    public Transform cat;
    private Vector3 catPos;
    private Quaternion catRotation;

    void Start()
    {
        catPos = cat.position;
        catRotation = cat.transform.rotation;
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag != "collectible" )
        {
            Debug.Log("Collision detected");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //handles enemy collisions
        if (other.gameObject.tag == "enemy")
        {
            lives--;
            Debug.Log(lives);
            //respawn to the beginning 
            gameObject.transform.rotation = catRotation;
            gameObject.transform.position = catPos;
            
        }
    }

    public void Shove(){

    }
}
