using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    void Start()
    {
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
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            RestartPosition();
        }
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
}
