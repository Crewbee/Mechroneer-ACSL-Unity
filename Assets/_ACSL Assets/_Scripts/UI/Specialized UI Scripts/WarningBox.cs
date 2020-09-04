using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningBox : MonoBehaviour
{
    private CanvasGroup m_CanvasGroup;

    public TextMeshProUGUI warningText;
    private string m_WarningMessage;


    void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CanvasGroup.alpha = 0f;
        m_CanvasGroup.interactable = false;
        m_CanvasGroup.blocksRaycasts = false;

        if (warningText == null)
            warningText = GetComponent<TextMeshProUGUI>();

        m_WarningMessage = warningText.text;
    }

    void Update()
    {
        
    }

    public void ChangeText(string text)
    {
        m_WarningMessage = text;
        warningText.text = m_WarningMessage;
    }

    public void Display()
    {
        gameObject.SetActive(true);
        StartCoroutine(Display(m_CanvasGroup, 2f));
    }

    IEnumerator Display(CanvasGroup canvasGroup, float time)
    {
        StartCoroutine(FadeIn(canvasGroup, 0.1f));

        yield return new WaitForSeconds(time);

        StartCoroutine(FadeOut(canvasGroup, 0.1f));
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup, float time)
    {
        float alpha = 0f;
        float t = 0f;
        while (t < time)
        {
            float blend = Mathf.Clamp01(t / time);

            t += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(alpha, 1f, blend);

            yield return null;
        }
    }

    IEnumerator FadeOut(CanvasGroup canvasGroup, float time)
    {
        float alpha = 1f;
        float t = 0f;
        while (t < time)
        {
            float blend = Mathf.Clamp01(t / time);

            t += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(alpha, 0f, blend);

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
