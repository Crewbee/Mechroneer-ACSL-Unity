using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Layer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.layer == 9)
        {
            Debug.Log("MaskChanged");
        }
    }
}
