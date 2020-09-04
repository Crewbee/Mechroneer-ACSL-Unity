using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMaterial : MonoBehaviour
{
    public float scrollSpeed;
    float offset;
    MeshRenderer m_renderer;
    public bool up;
    public bool right;
    private RobotLeg treads;

    // Start is called before the first frame update
    void Start()
    {
        offset = 0.0f;
        m_renderer = GetComponent<MeshRenderer>();
        if(m_renderer == null)
        {
            Debug.Log("failed");
        }
        if(up)
        {
            treads = GetComponentInParent<RobotLeg>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10.0f;
        if (m_renderer)
        {
            //Debug.Log("Renderer found");
            if (up)
            {
                // 
                if (treads)
                {
                    m_renderer.material.SetTextureOffset("_BaseMap", new Vector2(0, offset* treads.speed));
                }
            }
            if(right)
            {
                //Debug.Log("Right");
                m_renderer.material.SetTextureOffset("_BaseMap", new Vector2(offset, 0));
            }
        }
    }
}

//scroll main texture based on time


