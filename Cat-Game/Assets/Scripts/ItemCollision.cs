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

    //check for collissions with item
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "collectible")
        {
           //increase collected item count
            collectedItems++;
           
            //pass item to CheckItems 
            CheckItems(other.gameObject);
            //make object inactive, since we still need information for CheckItems function
            other.gameObject.SetActive(false);
        }
    }

    //checks whether all items were collected 
    //and if player can proceed
    //also handles level transition 
    private void CheckItems(GameObject item)
    {
        if (collectedItems >= 3)
        {
            levelPassage.SetActive(true);
        }
        else if( scene.name == "Level1")
        {
            if(item)
            {
                Debug.Log("Ok");
            }
        }
    }
}
