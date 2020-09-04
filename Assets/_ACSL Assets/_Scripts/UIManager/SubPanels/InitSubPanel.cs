using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InitSubPanel : UISubPanel
{
    public UIManager mainUIManager;
    public UIPanel mainUIPanel;
    static bool m_hasInit = false;
    public GameObject username;

    //protected override void OnActivated()
    //{
    //    base.OnActivated();
    //    if (m_hasInit)
    //    {
    //        mainUIManager.Push(mainUIPanel);
    //    }
    //}
    private void OnEnable()
    {
        //Debug.Log("OnEnable: " + m_hasInit);
        if (m_hasInit)
        {
            mainUIManager.Push(mainUIPanel);
        }
        
    }
    private  void OnDisable()
    {
        //Debug.Log("OnDisable: " + m_hasInit);
        m_hasInit = true;
    }
    //protected override void OnDeactivated()
    //{
    //    base.OnDeactivated();
    //    m_hasInit = true;
    //}

    // Start is called before the first frame update
    void Start()
    {
        if(UserData._instance.signedIN == false)
        {
            //Debug.Log("Usersigned in");
            UserData._instance.signedIN = true;
        }
        else
        {
            gameObject.SetActive(false);
            //Debug.Log("pushing");
            mainUIManager.Pop();
            mainUIManager.gameObject.SetActive(true);
            mainUIManager.Push(mainUIPanel);
            FindObjectOfType<MenuCamera>().currentCamState = MenuCameraState.MCS_LOGINSTART;
            if (username.GetComponent<TextMeshProUGUI>())
            {
                    username.GetComponent<TextMeshProUGUI>().text = UserData._instance.Username;
            }
            else
            {
                Debug.Log("failed to get TextMeshProUGUI");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Has init: " + m_hasInit);
    }
}
