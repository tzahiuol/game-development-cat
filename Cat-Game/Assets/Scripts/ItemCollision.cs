using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.GetActiveScene.html
using UnityEngine.SceneManagement;


public class ItemCollision : MonoBehaviour
{
    private int collectedItems;
    public GameObject levelPassage;
    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        collectedItems = 0;
        //https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.GetActiveScene.html
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectible"))
        {
            string item = other.tag;
          
            collectedItems++;
            Destroy(gameObject);
            CheckItems(item);
        }
    }

    private void CheckItems(string item)
    {
        if (collectedItems >= 3)
        {
            levelPassage.SetActive(true);
        }
        else if( scene.name == "Level2")
        {
            if(item == "Item2")
            {
                Debug.Log("Ok");
            }
        }
    }
}
