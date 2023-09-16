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

    private Timer timer;
    private HeartManager heartManager;

    private GameObject gameOverPanel;

    //audio sources 
    [SerializeField]
    private AudioSource audioSource;
    public AudioClip hit;
    public AudioClip gameOver;

    void Start()
    {
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
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Losing life, now at: " + lives);
        heartManager.LoseHeart();
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
    }

    public void Shove(){

    }
    //takes the sound clip as an argument
    void PlaySound(AudioClip soundClip)
    {
        audioSource.clip = soundClip;
        audioSource.Play();
    }
}
