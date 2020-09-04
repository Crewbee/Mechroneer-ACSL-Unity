using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageManager : MonoBehaviour
{

    #region Singleton

    public static GarageManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public UserData playerData;
    public List<RobotData> availableRobots;

    public MenuCamera menuCam;
    public AudioManager audioManager;

    public Canvas initUI;
    public Canvas navUI;
    public Canvas mainUI;

    private bool m_FirstStart;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.PlaySoundFrom("Menu Music", GetComponent<AudioSource>());
        initUI.gameObject.SetActive(true);
        mainUI.gameObject.SetActive(false);

        m_FirstStart = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.m_AuthNeeded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initUI.gameObject.SetActive(false);
                mainUI.gameObject.SetActive(true);
                navUI.gameObject.SetActive(true);
                menuCam.currentCamState = MenuCameraState.MCS_LOGINSTART;
                m_FirstStart = false;
            }
        }
    }

    public RobotData GetRobot(int index)
    {
        if (index < availableRobots.Count)
        {
            return availableRobots[index];
        }
        else
        {
            return null;
        }
    }
}
