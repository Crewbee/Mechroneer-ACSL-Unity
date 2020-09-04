using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoorScript : MonoBehaviour
{
    public Light[] outdoorLights = new Light[2];

    public Vector3 endPos;
    public Vector3 startPos;
    public Vector3 endRot;

    public bool START = false;

    // Start is called before the first frame update
    void Start()
    {
        if (UserData._instance.signedIN == false)
        {
            transform.position = startPos;
            START = false;
        }
        else
        {
            transform.position = endPos;
            transform.rotation = Quaternion.Euler(endRot);
        }
        
    }

    // Update is called once per frame.
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            START = true;
        }
        if (START)
        {
            foreach(var light in outdoorLights)
            {
                light.enabled = false;
            }

            Vector3 currentpos = transform.position;

            currentpos = Vector3.Lerp(currentpos, endPos, Time.deltaTime * 1.5f);

            transform.position = currentpos;

            Vector3 currentrot = transform.rotation.eulerAngles;

            currentrot = Vector3.Lerp(currentrot, endRot, Time.deltaTime * 1.5f);

            transform.rotation = Quaternion.Euler(currentrot);
        }
    }
}