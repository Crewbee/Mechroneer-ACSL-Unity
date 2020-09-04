using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToMoveArrow : MonoBehaviour
{
    public GameObject m_dropletPrefab;
    public GameObject m_droplet;
    public bool b_isMoving;
    public float m_dropletAlpha;

    // Start is called before the first frame update
    void Start()
    {
        b_isMoving = false;
        gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
        m_droplet = Instantiate(m_dropletPrefab, this.transform.position, Quaternion.identity, transform);
        gameObject.SetActive(false);
        m_dropletAlpha = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(b_isMoving == true)
        {
            if (m_droplet)
            {
                m_droplet.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * m_dropletAlpha;
                Color tempMatColor = m_droplet.GetComponent<Renderer>().material.color;
                m_droplet.GetComponent<Renderer>().material.color = new Color(tempMatColor.r, tempMatColor.g, tempMatColor.b, m_dropletAlpha);
                m_dropletAlpha -= 0.1f;
                if(m_dropletAlpha < 0.0f)
                {
                    gameObject.SetActive(false);
                    b_isMoving = false;
                }
            }
        }
    }

    public void SetPosition(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        m_dropletAlpha = 0.5f;
        b_isMoving = true;
    }
}
