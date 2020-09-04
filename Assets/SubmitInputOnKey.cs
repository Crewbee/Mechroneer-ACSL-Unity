using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitInputOnKey : MonoBehaviour
{
    public Button loginButton;
    private InputField m_InputField;

    void Awake()
    {
        m_InputField = GetComponent<InputField>();
    }

    private void inputSubmitCallBack()
    {
        loginButton.onClick.Invoke();
    }

    private void inputChangedCallBack()
    {
    }

    void OnEnable()
    {
        m_InputField.onEndEdit.AddListener(delegate { inputSubmitCallBack(); });
        m_InputField.onValueChanged.AddListener(delegate { inputChangedCallBack(); });
    }

    void OnDisable()
    {
        m_InputField.onEndEdit.RemoveAllListeners();
        m_InputField.onValueChanged.RemoveAllListeners();
    }
}
