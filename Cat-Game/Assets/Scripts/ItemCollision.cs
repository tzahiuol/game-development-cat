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


    public Animator keyAnimation;


    //audio sources 
    [SerializeField]
    private AudioSource audioSource;
    public AudioClip allItems;
    public AudioClip itemCollected;

    // Start is called before the first frame update
    void Start()
    {
        //initialise audio source
        audioSource = this.GetComponent<AudioSource>();

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
        Debug.Log(other);
        if (other.gameObject.tag == "collectible")
        {
            
            PlaySound(itemCollected);
            //increase collected item count
            collectedItems++;
         
            //pass item to CheckItems 
            CheckItems(other.gameObject);
            
            other.gameObject.GetComponent<Animator>().SetTrigger("Collected");
            GameObject.FindObjectOfType<KeysManager>().FoundKey();
        }
        if(other.gameObject.tag == "transition") //check for collission with transition collider
        {
            Debug.Log("Scene Transition");
            StartCoroutine(LoadNextLevelAfterDelay(2.0f, 2));
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
            //plays the sound to signify that all items were collected
            PlaySound(allItems);

        }
        else if( scene.name == "Level1")
        {
            if(item)
            {
                Debug.Log("Ok");
            }
        }
    }

    //https://discussions.unity.com/t/what-are-ienumerator-and-coroutine/143510/2
    IEnumerator LoadNextLevelAfterDelay(float delay, int level)
    {
        //https://docs.unity3d.com/ScriptReference/WaitForSeconds.html
        yield return new WaitForSeconds(delay); //to delay the transition of the levels

        string prefixLevel = "Level";
        SceneManager.LoadScene(prefixLevel + level, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(prefixLevel + level);

        GameObject.FindObjectOfType<KeysManager>().Restart();
    }

    void PlaySound(AudioClip soundClip)
    {
        audioSource.clip = soundClip;
        audioSource.Play();
    }
}
