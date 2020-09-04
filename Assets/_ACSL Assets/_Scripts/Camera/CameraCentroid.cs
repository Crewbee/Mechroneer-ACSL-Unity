using System.Collections.Generic;
using UnityEngine;

public class CameraCentroid : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    [Header("Triangulation Settings")]
    public List<Transform> m_Targets = new List<Transform>();
    [HideInInspector]
    public Transform m_Centroid;
    public bool m_UseMousePosition = false;
    #endregion

    private CameraCentroid(Transform[] transforms)
    {
        m_Targets.AddRange(transforms);
    }

    private void Awake()
    {
        Transform temp = new GameObject().transform;
        temp.transform.name = "Camera Centroid";
        m_Centroid = temp;

        //Transform temp2 = new GameObject().transform;
        //temp2.transform.name = "Current Centroid Target";
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Initial calculation
        if (m_Targets.Count > 0)
        {
            m_Centroid.position = CalculateCentroid(m_Targets.ToArray());
        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        for(int i = 0; i < m_Targets.Count; i++)
        {
            if (!m_Targets[i])
            {
                m_Targets.RemoveAt(i);
            }
        }

        // Calculate the centroid
        if (m_Centroid)
        {
            m_Centroid.position = Vector3.Slerp(m_Centroid.position, CalculateCentroid(m_Targets.ToArray()), Time.deltaTime * 5f);
        }
    }

    private Vector3 CalculateCentroid(Transform[] transforms)
    {
        Vector3 center = Vector3.zero;
        foreach (Transform transform in transforms)
        {
            if (transform)
            {
                center += transform.position;
            }
        }

        if (m_UseMousePosition)
        {
            ///DEBUG REMOVE ME
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                return (center += hitInfo.point /= transforms.Length + 1);
            }
            else
            {
                return (center /= transforms.Length);
            }
            ///END REMOVE
        }
        else if (transforms.Length > 0)
        {
            return (center /= transforms.Length);
        }
        else
            return Vector3.zero;
    }

    private float GetMaxDistance(Transform[] transforms)
    {
        float maxDistance = 0f;

        for (int i = 0; i < transforms.Length; i++)
        {
            if (i + 1 >= transforms.Length)
            {
                break;
            }

            float distance = Vector3.Distance(transforms[i].position, transforms[i + 1].position);
            if (distance > maxDistance)
            {
                Debug.Log("Furthest GameObjects: " + transforms[i].name + " & " + transforms[i + 1].name);
                maxDistance = distance;
            }
            else
            {
                continue;
            }
        }

        return maxDistance;
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying && isActiveAndEnabled)
        {
            // Draw Center point
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(m_Centroid.position, 0.1f);

            // Draw lines
            for (int i = 0; i < m_Targets.Count; i++)
            {
                if (i + 1 >= m_Targets.Count)
                {
                    Gizmos.DrawLine(m_Targets[m_Targets.Count - 1].position, m_Targets[0].position);
                    break;
                }

                Gizmos.DrawLine(m_Targets[i].position, m_Targets[i + 1].position);
            }
        }
    }
}
