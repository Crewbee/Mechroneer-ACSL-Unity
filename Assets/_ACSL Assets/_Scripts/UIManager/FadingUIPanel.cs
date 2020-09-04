using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadingUIPanel : UIPanel
{
    public float fadeTime = 0.25f;
    private CanvasGroup canvasGroup;
    public delegate void StackAction();

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    //Called when this panel gets pushed to stack
    public override void OnPushed()
    {
        base.OnPushed();
        FadeIn();
    }

    //Called when this panel gets popped off the stack
    public override void OnPopped()
    {
        FadeOut(base.OnPopped);
    }

    //Called when a panel was pushed on top of this panel on the stack
    public override void OnPushedOnTop()
    {
        FadeOut(base.OnPushedOnTop);
    }

    //Called when the panel above it gets popped off
    public override void OnPoppedTo()
    {
        base.OnPoppedTo();
        FadeIn();
    }

    virtual protected void FadeIn()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(FadeIn(canvasGroup, fadeTime));
    }

    virtual protected void FadeOut(StackAction action)
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(FadeOut(canvasGroup, fadeTime, action));
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

    protected IEnumerator FadeOut(CanvasGroup cg, float time, StackAction action)
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
        action();
    }
}
