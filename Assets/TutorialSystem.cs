using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Tutorial
{
    public string name;
    public GameObject content;
}
public class TutorialSystem : MonoBehaviour
{
    // Public
    public List<Tutorial> Tutorials = new List<Tutorial>();
    public List<GameObject> popUps = new List<GameObject>();

    // Private
    private Tutorial m_CurrentTutorial;
    private TutorialRobot m_Robot;
    private UnityAction m_NextPopUpAction;

    private int m_TutorialIndex = 0;
    private readonly InputListener m_CurrentListener;
    private RobotListener m_CurrentRobotListener;

    private void Start()
    {
        // Get observed robot
        m_Robot = FindObjectOfType<TutorialRobot>();

        // Start at 0
        m_TutorialIndex = 0;

        // Assign action
        m_NextPopUpAction += NextStep;

        // Disable all tutorials
        foreach (Tutorial Tutorial in Tutorials)
        {
            Tutorial.content.SetActive(false);
        }

        // Determine tutorial to show (platform dependent)
        if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            // Determine controller active.
            if (InputManager.instance.JoystickCount > 0)
            {
                Tutorial tut = Tutorials.Find(x => x.name == "Controller");
                if (tut != null)
                {
                    tut.content.SetActive(true);
                    m_CurrentTutorial = tut;
                }
            }
            else // If controller not active
            {
                Tutorial tut = Tutorials.Find(x => x.name == "Keyboard");
                if (tut != null)
                {
                    tut.content.SetActive(true);
                    m_CurrentTutorial = tut;
                }
            }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer ||
                 Application.platform == RuntimePlatform.Android)
        {
            // Start mobile tutorial
            Tutorial tut = Tutorials.Find(x => x.name == "Mobile");
            if (tut != null)
            {
                tut.content.SetActive(true);
                m_CurrentTutorial = tut;
            }
        }

        //Populate Pop-Up list
        foreach (Transform popUp in m_CurrentTutorial.content.transform)
        {
            popUps.Add(popUp.gameObject);
            //child.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        //// Listen to first InputListener
        //if (popUps[m_TutorialIndex].TryGetComponent<InputListener>(out m_CurrentListener))
        //{
        //    m_CurrentListener.allButtonsPressed += StepCompleted;
        //}

        // Assign each listener a ref to observed robot
        foreach (GameObject popUp in popUps)
        {
            RobotListener rl = popUp.GetComponent<RobotListener>();
            if (rl)
            {
                rl.robot = m_Robot;
            }
        }

        // Listen to first RobotListener
        if (popUps[m_TutorialIndex].TryGetComponent<RobotListener>(out m_CurrentRobotListener))
        {
            m_CurrentRobotListener.requirementsMet += StepCompleted;
        }

        // Display first popUp
        for (int i = 0; i < popUps.Count; i++)
        {
            if (i == m_TutorialIndex)
            {
                StartCoroutine(FadeIn(popUps[i].GetComponent<CanvasGroup>(), 0.25f));
            }
            else
            {
                popUps[i].GetComponent<CanvasGroup>().alpha = 0f;
                popUps[i].GetComponent<CanvasGroup>().interactable = false;
                popUps[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
                popUps[i].GetComponent<CanvasGroup>().gameObject.SetActive(false);
            }
        }
    }

    private void StepCompleted(bool status)
    {
        // If Step is completed
        if (status == true)
        {
            // Iterate through popUps
            NextStep();
        }
        else
        {
            return; // Never should hit.
        }
    }

    private void StepCompleted()
    {
        // Iterate through popUps
        NextStep();
    }

    public void NextStep()
    {
        if (m_TutorialIndex + 1 >= popUps.Count)
        {
            EndTutorial();
            return;
        }

        // Fade current step
        StartCoroutine(FadeOut(popUps[m_TutorialIndex].GetComponent<CanvasGroup>(), 0.25f));

        m_TutorialIndex++;

        // Fade in next step
        StartCoroutine(FadeIn(popUps[m_TutorialIndex].GetComponent<CanvasGroup>(), 0.25f));


        //// Listen to new InputListener
        //if (popUps[m_TutorialIndex].TryGetComponent<InputListener>(out m_CurrentListener))
        //{
        //    m_CurrentListener.allButtonsPressed += StepCompleted;
        //}

        // Listen to new RobotListener
        if (popUps[m_TutorialIndex].TryGetComponent<RobotListener>(out m_CurrentRobotListener))
        {
            m_CurrentRobotListener.requirementsMet += StepCompleted;
        }
    }

    public void EndTutorial()
    {
        // Close all tutorial windows
        foreach (GameObject popUp in popUps)
        {
            StartCoroutine(FadeOut(popUp.GetComponent<CanvasGroup>(), 0.25f));
        }
    }

    public void RestartTutorial()
    {
        // Close all tutorial windows and call Start()
        EndTutorial();
        Start();
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup, float time)
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 0f;
        float alpha = canvasGroup.alpha;
        float t = 0f;
        while (t < time)
        {
            float blend = Mathf.Clamp01(t / time);

            t += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(alpha, 1f, blend);

            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup, float time)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 1f;
        float alpha = canvasGroup.alpha;
        float t = 0f;
        while (t < time)
        {
            float blend = Mathf.Clamp01(t / time);

            t += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(alpha, 0f, blend);

            yield return null;
        }
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
}
