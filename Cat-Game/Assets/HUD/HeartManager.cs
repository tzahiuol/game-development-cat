using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    public UnityEngine.UI.RawImage[] hearts;
    public Texture full_heart;
    public Texture missing_heart;

    private int currentHeart;

    // Start is called before the first frame update
    void Start()
    {
        Restart();
    }

    public void Restart()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].texture = full_heart;
        }
        currentHeart = 0;
    }

    public void LoseHeart()
    {
        Debug.Log("Losing heart: " + currentHeart);
        if (currentHeart != hearts.Length){
            hearts[currentHeart].texture = missing_heart;
        }
        currentHeart++;
    }
}
