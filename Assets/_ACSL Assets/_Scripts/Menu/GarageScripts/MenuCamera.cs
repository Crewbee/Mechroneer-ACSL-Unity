using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuCameraState
{
    MCS_MENUSTART = 0,
    MCS_LOGINSTART,
    MCS_ROBOTSELECT
}

public class MenuCamera : MonoBehaviour
{

    public Vector3 startingPosition = Vector3.zero;
    public Vector3 endPosition = Vector3.zero;
    public Vector3 robotSelectPos = Vector3.zero;

    public Vector3 robotSelectRotation = new Vector3(9.895f, 265.609f, 0.0f);
    public Vector3 menuCameraStartRot = Vector3.zero;


    public MenuCameraState currentCamState;
    public UIPanel panel;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPosition;

        menuCameraStartRot = transform.rotation.eulerAngles;

        currentCamState = MenuCameraState.MCS_MENUSTART;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCamState == MenuCameraState.MCS_MENUSTART)
        {
            if (Input.GetMouseButton(0))
            {
                currentCamState = MenuCameraState.MCS_LOGINSTART;
            }

        }

        switch (currentCamState)
        {

            case MenuCameraState.MCS_MENUSTART:

                break;

            case MenuCameraState.MCS_LOGINSTART:

                if (transform.position.z - endPosition.z > 0.1f)
                {
                    Vector3 NewPosition = Vector3.Slerp(transform.position, endPosition, Time.deltaTime * 1.2f);

                    transform.position = NewPosition;

                    if (transform.rotation != Quaternion.Euler(menuCameraStartRot))
                    {
                        Quaternion LookDir = Quaternion.Euler(menuCameraStartRot);

                        Quaternion currentRot = Quaternion.Slerp(transform.rotation, LookDir, Time.deltaTime * 1.2f);

                        if (transform.rotation.eulerAngles.x - menuCameraStartRot.x < 0.1f)
                        {
                            transform.rotation = Quaternion.Euler(menuCameraStartRot);
                        }
                    }
                }
               
                if(panel.gameObject.activeInHierarchy == true)
                {
                    currentCamState = MenuCameraState.MCS_ROBOTSELECT;
                }

                break;

            case MenuCameraState.MCS_ROBOTSELECT:

                if (transform.position.z - robotSelectPos.z > 0.1f)
                {
                    Quaternion LookDir = Quaternion.Euler(robotSelectRotation);

                    Quaternion currentRot = Quaternion.Slerp(transform.rotation, LookDir, Time.deltaTime * 1.2f);

                    Vector3 currentPosition = Vector3.Slerp(transform.position, robotSelectPos, Time.deltaTime * 1.2f);

                    transform.rotation = currentRot;
                    transform.position = currentPosition;
                }

                break;
        }
    }

    void SwitchMenuCamState(MenuCameraState aNewState)
    {
        currentCamState = aNewState;
    }
}
