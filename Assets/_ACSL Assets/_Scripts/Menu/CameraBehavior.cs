using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    public enum CameraState
    {
        CS_CAMCENTER = 0,
        CS_CAMLEFTMAIN,
        CS_CAMRIGHTMAIN,
        CS_CAMRIGHTTOPDRAWER,
        CS_CAMLEFTTOPDRAWER,
        CS_CAMRIGHTBOTTOMDRAWER,
        CS_CAMLEFTBOTTOMDRAWER,
        CS_CAMRIGHTCABINET,
        CS_CAMLEFTCABINET
    }

    public enum MenuState
    {
        MS_NULL = -1,
        MS_CENTER = 0,
        MS_RIGHT,
        MS_LEFT
    }

    public Transform[] PossibleTargets = new Transform[6];
    public Transform CamTarget;

    public Vector3[] CamPositions;
    public Vector3[] CamRotations;

    public float CamTransTime = 1.0f;
    public float CamRotSpd = 0.1f;


    public MenuState m_CurrentState;

    public CameraState m_CamState;


    // Start is called before the first frame update
    void Start()
    {
        CamPositions[0] = new Vector3(0.0f, 3.08f, -7.35f);
        CamPositions[1] = new Vector3(-2.15f, 0.26f, -2.51f);
        CamPositions[2] = new Vector3(2.15f, 0.26f, -2.51f);
        CamPositions[3] = new Vector3(0.514f, 1.865f, -0.874f);
        CamPositions[4] = new Vector3(-0.514f, 1.865f, -0.874f);
        CamPositions[4] = new Vector3(0.479f, 1.594f, -0.839f);
        CamPositions[4] = new Vector3(-0.479f, 1.594f, -0.839f);
        CamPositions[5] = new Vector3(2.173f, 0.247f, -1.819f);
        CamPositions[5] = new Vector3(-2.173f, 0.247f, -1.819f);


        CamRotations[0] = new Vector3(20, 0, 0);
        CamRotations[1] = new Vector3(0, 45, 0);
        CamRotations[2] = new Vector3(0, -45, 0);
        CamRotations[3] = new Vector3(80, -45, 0);
        CamRotations[4] = new Vector3(80, 45, 0);
        CamRotations[4] = new Vector3(78, -45, 0);
        CamRotations[4] = new Vector3(78, 45, 0);
        CamRotations[5] = new Vector3(0, -45, 0);
        CamRotations[5] = new Vector3(0, 45, 0);

        m_CamState = CameraState.CS_CAMCENTER;

        m_CurrentState = MenuState.MS_NULL;

    }

    // Update is called once per frame
    void Update()
    {
        switch (m_CamState)
        {
            case CameraState.CS_CAMCENTER:

                break;

            case CameraState.CS_CAMLEFTMAIN:

                break;

            case CameraState.CS_CAMRIGHTMAIN:

                break;

            case CameraState.CS_CAMRIGHTTOPDRAWER:

                break;

            case CameraState.CS_CAMLEFTTOPDRAWER:

                break;

            case CameraState.CS_CAMRIGHTBOTTOMDRAWER:

                break;

            case CameraState.CS_CAMLEFTBOTTOMDRAWER:

                break;

            case CameraState.CS_CAMRIGHTCABINET:

                break;

            case CameraState.CS_CAMLEFTCABINET:

                break;
        }

    }

    void SetTarget(Vector3 newRot, Vector3 newPos)
    {

    }

    void CheckForMouseInput(MenuState currentState)
    {

    }
}
