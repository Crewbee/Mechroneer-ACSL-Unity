using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public Material selectionOutlineMaterial;
    private List<Material> m_originalMaterials = new List<Material>();
    private List<Material> m_newMaterials = new List<Material>();
    private Renderer m_renderer;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_originalMaterials.AddRange(m_renderer.materials);
    }

    void Update()
    {
        float sin = Mathf.Abs(Mathf.Sin(Time.time) * 0.05f) + 0.1f;
        selectionOutlineMaterial.SetFloat("_Width", sin);
    }

    public void Select()
    {
        m_newMaterials.AddRange(m_originalMaterials);
        m_newMaterials.Add(selectionOutlineMaterial);
        m_renderer.materials = m_newMaterials.ToArray();
    }

    public void Deselect()
    {
        m_newMaterials.Clear();
        m_renderer.materials = m_originalMaterials.ToArray();
    }
}
