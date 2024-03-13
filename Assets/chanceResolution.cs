using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chanceResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = new Vector3((float)(Screen.width * 0.09), (float)(Screen.height * 0.07), 1);
        transform.localScale = v;
    }
}
