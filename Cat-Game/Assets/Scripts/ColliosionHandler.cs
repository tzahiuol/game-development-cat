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

    private Timer timer;
    void Start()
    {
        catPos = cat.position;
        catRotation = cat.transform.rotation;
        timer = FindObjectOfType<Timer>();
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag != "collectible" )
        {
            Debug.Log("Collision detected");
        }
    }

    void Update()
    {
        if (gameObject.transform.position.y < -10)
        {
            Restart();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //handles enemy collisions
        if (other.gameObject.tag == "enemy")
        {
            lives--;
            Debug.Log(lives);
            Restart();
        }
    }

    public void Restart()
    {
        gameObject.transform.rotation = catRotation;
        gameObject.transform.position = catPos;
        timer.Restart();
    }

    public void Shove(){

    }
}
