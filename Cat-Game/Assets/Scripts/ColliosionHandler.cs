using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColliosionHandler : MonoBehaviour
{
    //count the cats lives
    [SerializeField]
    private int initialLives = 3;

    private int lives;

    public Transform cat;
    private Vector3 catPos;
    private Quaternion catRotation;
    private CatActions catActionScript;

    private Timer timer;
    private HeartManager heartManager;

    private GameObject gameOverPanel;

    //audio sources 
    [SerializeField]
    private AudioSource audioSource;
    public AudioClip hit;
    public AudioClip gameOver;

    //transparentMaterial
    public Material transparentMaterial;

    void Start()
    {
        catActionScript = GetComponent<CatActions>();
        //initialise audio source
        audioSource = GetComponent<AudioSource>();
  
        lives = initialLives;
        catPos = cat.position;
        catRotation = cat.transform.rotation;
        timer = FindObjectOfType<Timer>();
        heartManager = FindObjectOfType<HeartManager>();
        gameOverPanel = GameObject.Find("GameOver");

        gameOverPanel.SetActive(false);
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
            LoseLife();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //handles enemy collisions
        if (other.gameObject.tag == "enemy")
        {
            LoseLife();
            
        }
        if(other.gameObject.tag == "TransparentTrigger") //https://stackoverflow.com/questions/48255184/how-to-change-the-material-on-a-trigger#:~:text=1%20Answer&text=Get%20the%20Renderer%20component%20on,(Collider%20other)%20%7B%20other.
        {
            //change the material of object with tag transparentobject
            Debug.Log("In Tunnel");
            GameObject[] transparentObjects  = GameObject.FindGameObjectsWithTag("TransparentObject");

            foreach (GameObject transparentObject in transparentObjects)
            {
                Renderer rendering = transparentObject.GetComponent<Renderer>();
                if (rendering != null)
                {
                    rendering.material = transparentMaterial;
                }
            }
        }

    }

    public void LoseLife()
    {
        
        lives--;
        Debug.Log("Losing life, now at: " + lives);
        heartManager.LoseHeart();
        GetComponent<CatActions>().setDeath(true);
        GetComponent<CatActions>().enabled = false;
        Invoke("killKitty", 2f);
        if (lives == 0)
        {
            EndGame(false);
        }
        else
        {
            PlaySound(hit);
            RestartPosition();
        }

    }

    public void EndGame(bool isByTimer)
    {
        gameOverPanel.SetActive(true);
        PlaySound(gameOver);
        string text = "Cats normally have 9 lives, you had 3..";
        if (isByTimer)
        {
            text = "10 minutes wasn't enough?";
        }
        var textChildren = gameOverPanel.GetComponentsInChildren<Text>();
        for (int i = 0; i < textChildren.Length; i++)
        {
            if (textChildren[i].name == "DescriptionText")
            {
                textChildren[i].text = text;
            }
        }

        Time.timeScale = 0f;
    }

    private void RestartPosition()
    {
        gameObject.transform.rotation = catRotation;
        gameObject.transform.position = catPos;
    }

    public void Restart()
    {
        timer.Restart();
        RestartPosition();
        heartManager.Restart();
        lives = initialLives;
        GetComponent<CatActions>().setDeath(false);
        GetComponent<CatActions>().enabled = true;
        GameObject.FindObjectOfType<KeysManager>().Restart();
        SceneManager.LoadScene("Level1"); // reload scene
    }

    public void Shove(){

    }

    private void killKitty()
    {
        if (lives == 0)
        {
            EndGame(false);
        }
        else
        {
            RestartPosition();
            GetComponent<CatActions>().setDeath(false);
            GetComponent<CatActions>().enabled = true;
        }

    }
    //takes the sound clip as an argument
    void PlaySound(AudioClip soundClip)
    {
        audioSource.clip = soundClip;
        audioSource.Play();
    }


}
