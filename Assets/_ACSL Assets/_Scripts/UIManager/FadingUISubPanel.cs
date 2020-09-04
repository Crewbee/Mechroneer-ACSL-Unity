using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class FadingUISubPanel : UISubPanel
{
    public float fadeTime = 0.25f;
    private CanvasGroup canvasGroup;


    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    protected override void OnActivated()
    {
        base.OnActivated();
        FadeIn();
    }

    protected override void OnDeactivated()
    {
        FadeOut();
    }

    virtual protected void FadeIn()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(FadeIn(canvasGroup, fadeTime));
    }

    virtual protected void FadeOut()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(FadeOut(canvasGroup, fadeTime));
    }
    protected IEnumerator FadeIn(CanvasGroup cg, float time)
    {
        cg.interactable = true;
        cg.blocksRaycasts = true;
        cg.alpha = 0f;
        float alpha = cg.alpha;
        float t = 0f;
        while (t < time)
        {
            float blend = Mathf.Clamp01(t / time);

            t += Time.deltaTime;

            cg.alpha = Mathf.Lerp(alpha, 1f, blend);

            yield return null;
        }
        cg.alpha = 1f;
    }

    protected IEnumerator FadeOut(CanvasGroup cg, float time)
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;
        cg.alpha = 1f;
        float alpha = cg.alpha;
        float t = 0f;
        while (t < time)
        {
            float blend = Mathf.Clamp01(t / time);

            t += Time.deltaTime;

            cg.alpha = Mathf.Lerp(alpha, 0f, blend);

            yield return null;
        }
        cg.alpha = 0f;
        base.OnDeactivated();
    }
}
