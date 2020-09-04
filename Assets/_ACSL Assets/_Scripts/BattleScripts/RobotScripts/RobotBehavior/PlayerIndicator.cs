using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    GameObject m_obj;

    // Start is called before the first frame update
    void Start()
    {
        //EnableVisuals();
    }

    public void ChangeTarget(GameObject obj)
    {
        m_obj = obj;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 120.0f * Time.deltaTime));
        if (m_obj)
        {
            Vector3 pos = m_obj.transform.position;
            pos.y += 4.0f;
            transform.position = pos;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

    public void DisableVisuals()
        {
        GetComponent<Renderer>().enabled = false;
        }

    public void EnableVisuals()
    {
        GetComponent<Renderer>().enabled = true;
    }
}
