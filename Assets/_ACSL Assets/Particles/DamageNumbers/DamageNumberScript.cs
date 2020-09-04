using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberScript : MonoBehaviour
{
    public GameObject owner;
    public float LifeTime = 3.0f;

    public Vector3 NumPosOffset = new Vector3(0.0f, 4.0f, 0.0f);

    public Vector3 RandomizeIntensity = new Vector3(2.5f, 0.0f, 0.0f);

    public float MoveToXAxis = 4.5f;
    public float MoveToYAxis = 4.5f;
    public float PositionEaseSpeed = 2.2f;

    private Camera m_Camera;

    private TextMeshPro m_Text;

    private float ScaleEaseSpeed = 2.2f;

    private Vector3 GoalPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;

        transform.position += NumPosOffset;

        transform.position += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(0.0f, RandomizeIntensity.y), Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));

        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, Vector3.up);

        GoalPosition = new Vector3(Random.Range(owner.transform.localPosition.x, owner.transform.localPosition.x + MoveToXAxis), 
            Random.Range(owner.transform.localPosition.y, owner.transform.localPosition.y + MoveToYAxis), owner.transform.localPosition.z);

        m_Text = GetComponent<TextMeshPro>();

        Destroy(gameObject, LifeTime);
    }

    private void Update()
    {
        Vector3 currentScale = transform.localScale;

        Vector3 scaleDown = new Vector3(0.01f, 0.01f, 0.01f);

        transform.localScale = Vector3.Slerp(currentScale, Vector3.zero, Time.deltaTime * ScaleEaseSpeed);

        Vector3 currentPos = transform.position;

        //transform.localPosition = Vector3.Slerp(currentPos, GoalPosition, Time.deltaTime * PositionEaseSpeed);
        transform.Translate(Vector3.up * Time.deltaTime * PositionEaseSpeed);
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, Vector3.up);
    }

    public void UpdateFont(float damage)
    {
        m_Text = GetComponent<TextMeshPro>();

        m_Text.SetText(damage.ToString());

        if(damage > 160.0f)
        {
            ScaleEaseSpeed *= 1.8f;
        }
        else if(damage > 60.0f)
        {
            ScaleEaseSpeed *= 1.5f;

        }
    }
}
