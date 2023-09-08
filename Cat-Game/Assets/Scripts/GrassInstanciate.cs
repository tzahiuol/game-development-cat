using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassInstanciate : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject grass;
    private GameObject bundle;

    //grass bundles 
    int bundlesAmount = 10;
    int bundleCount = 3;

    void Start()
    {
        grass = GameObject.FindWithTag("grass");

        //instantiate bundle
        //https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
        for (var i = 0; i < bundleCount; i++)
        {
            Instantiate(grass, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
