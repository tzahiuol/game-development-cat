using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysManager : MonoBehaviour
{

    public UnityEngine.UI.RawImage[] keys;
    public Texture full_key;
    public Texture empty_key;

    private int currentKey;

    // Start is called before the first frame update
    void Start()
    {
        Restart();
    }

    public void Restart()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].texture = empty_key;
        }
        currentKey = 0;
    }

    public void FoundKey()
    {
        if (currentKey != keys.Length)
        {
            keys[currentKey].texture = full_key;
        }
        currentKey++;
    }
}
