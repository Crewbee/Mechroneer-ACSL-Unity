using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
//
//public static class SceneLoader
//{
//    public static string m_SceneToLoad;
//    public static LoadSceneMode m_LoadSceneMode;

//    public static void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
//    {
//        // Fade out current scene. TODO
//        //GameObject.FindObjectOfType<SceneTransition>().FadeOut();

//        m_SceneToLoad = sceneName;
//        m_LoadSceneMode = loadSceneMode;

//        SceneManager.LoadScene("Loading");
//    }

//    public static void LoadScene(int sceneBuildIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
//    {
//        // Fade out current scene. TODO
//        //GameObject.FindObjectOfType<SceneTransition>().FadeOut();

//        m_SceneToLoad = SceneManager.GetSceneAt(sceneBuildIndex).name;
//        m_LoadSceneMode = loadSceneMode;

//        SceneManager.LoadScene("Loading");
//    }
//}

public class LoadingScreen : MonoBehaviourPun
{
    public static LoadingScreen _instance;

    [Header("Display")]
    public float MinDisplayTime = 1f;

    [Header("Text")]
    public TextMeshProUGUI m_LoadingText;
    [SerializeField]
    private TextMeshProUGUI m_LoadingPercent;

    [Header("Images")]
    [SerializeField]
    private Image m_ProgressBar;
    [SerializeField]
    private Image m_Spinner;
    [SerializeField]
    private Image m_MapPreview;

    [HideInInspector]
    public float loadingProgress;

    private bool m_SceneLoading;
    private float m_TimeElapsed;
    private CanvasGroup m_CanvasGroup;

    private int m_playersLoaded = 0;
    private bool m_playerHasLoaded;

    private void Awake()
    {
        // Singleton
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CanvasGroup.alpha = 0;
        m_CanvasGroup.blocksRaycasts = false;
        m_CanvasGroup.interactable = false;
    }

    private void Update()
    {
        if (m_SceneLoading)
        {
            if (LobbySettings.GetIsOnlineMatch())
            {
                PhotonUpdate();
            }
            else
            {
                UpdateProgressDisplay(loadingProgress);

                m_TimeElapsed += Time.deltaTime;

                if (loadingProgress == 0.9f && m_TimeElapsed >= MinDisplayTime)
                {
                    Hide();
                }
            }
        }
    }

    private void PhotonUpdate()
    {
        UpdateProgressDisplay(loadingProgress);

        m_TimeElapsed += Time.deltaTime;

        if (loadingProgress == 0.9f && !m_playerHasLoaded)
        {
            photonView.RPC("ServerPlayerLoaded", RpcTarget.MasterClient);
            m_playerHasLoaded = true;
        }
    }

    [PunRPC]
    private void NetworkHide()
    {
        StartCoroutine(FadeOut(GetComponent<CanvasGroup>(), 0.1f));
        m_SceneLoading = false;
        m_playerHasLoaded = false;
    }

    [PunRPC]
    private void ServerPlayerLoaded()
    {
        m_playersLoaded++;
        if (m_playersLoaded == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            m_playersLoaded = 0;
            MulticastNetworkHide();
        }
    }

    private void MulticastNetworkHide()
    {
        photonView.RPC("NetworkHide", RpcTarget.All);
    }

    public void Display()
    {
        StartCoroutine(FadeIn(m_CanvasGroup, 0.1f));

        //UpdateProgressDisplay(0f);

        m_TimeElapsed = 0f;

        m_SceneLoading = true;

        loadingProgress = 0f;
    }

    public void Hide()
    {
        StartCoroutine(FadeOut(m_CanvasGroup, 0.5f));
        m_SceneLoading = false;
    }

    private void UpdateProgressDisplay(float progress)
    {
        // Spin spinner
        m_Spinner.rectTransform.Rotate(-Vector3.forward * Time.deltaTime * 100f);
        m_Spinner.rectTransform.localScale = Vector3.one * Mathf.SmoothStep(0.5f, 1, Mathf.PingPong(Time.time, 1));

        // Flash %
        m_LoadingPercent.color = new Color(m_LoadingPercent.color.r, m_LoadingPercent.color.g, m_LoadingPercent.color.b, Mathf.SmoothStep(0.25f, 1, Mathf.PingPong(Time.time, 1)));

        // Update loading bar fill amount
        m_ProgressBar.fillAmount = Mathf.SmoothStep(m_ProgressBar.fillAmount, progress, Time.deltaTime * 10.0f);

        // Update %
        m_LoadingPercent.text = (progress * 100f) + "%";
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup, float time)
    {
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

    IEnumerator FadeOut(CanvasGroup canvasGroup, float time)
    {
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
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
