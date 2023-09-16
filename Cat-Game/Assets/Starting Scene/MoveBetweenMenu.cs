using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveBetweenMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject[] screenInOrder;

    public Text continueButtonText;
    private int currentIndex;
   
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        for (int i = 0; i < screenInOrder.Length; i++)
        {
            screenInOrder[i].SetActive(false);
        }
        screenInOrder[0].SetActive(true);
    }

    public void Next()
    {
        if (currentIndex + 1 != screenInOrder.Length)
        {
            screenInOrder[currentIndex].SetActive(false);
            screenInOrder[currentIndex + 1].SetActive(true);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        }
        currentIndex++;
        if (currentIndex + 1 == screenInOrder.Length)
        {
            continueButtonText.text = "Start game";
        }

    }
}
