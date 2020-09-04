using UnityEngine;

[DisallowMultipleComponent]
public class UISubPanel : MonoBehaviour
{
    public bool panelActiveOnAwake;
    public bool gameObjectCanBeDisabled;
    public bool active { get; private set; }

    private bool awakeCalled = false;
    virtual protected void Awake()
    {
        if (!awakeCalled)
        {
            awakeCalled = true;
            if (gameObjectCanBeDisabled)
                gameObject.SetActive(panelActiveOnAwake);
            else
                gameObject.SetActive(true);
            active = panelActiveOnAwake && gameObject.activeInHierarchy;
        }
        else
        {
            gameObject.SetActive(true);
            active = true;
        }
    }

    public void SetSubPanelActive(bool active)
    {
        if (this.active == active)
            return;

        if (active == true)
            OnActivated();
        else
            OnDeactivated();
    }
    public void ToggleSubPanel()
    {
        SetSubPanelActive(!active);
    }

    virtual protected void OnActivated()
    {
        if (!awakeCalled)
            awakeCalled = true;
        active = true;
        gameObject.SetActive(true);
    }

    virtual protected void OnDeactivated()
    {
        active = false;
        if (gameObjectCanBeDisabled)
            gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
