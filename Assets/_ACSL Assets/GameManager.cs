using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool m_AuthNeeded;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        m_AuthNeeded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
