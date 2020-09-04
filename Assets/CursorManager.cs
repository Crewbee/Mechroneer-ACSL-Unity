using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager _instance;
    public static CursorManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private Texture2D m_CursorTexture;
    public Texture2D cursorTexture
    {
        get { return m_CursorTexture; }

        set
        {
            m_CursorTexture = value;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }
    [SerializeField]
    private CursorMode m_CursorMode;
    public CursorMode cursorMode /*= CursorMode.Auto;*/
    {
        get { return m_CursorMode; }

        set
        {
            m_CursorMode = value;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }
    [SerializeField]
    private Vector2 m_HotSpot;
    public Vector2 hotSpot /*= Vector2.zero;*/
    {
        get { return m_HotSpot; }

        set
        {
            m_HotSpot = value;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
