using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Controller.IActions
{
    public GameObject GetGameObject()
    {
        if (this)
        {
            if (gameObject != null)
            {
                return gameObject;
            }
            else
            {
                return null;
            }
        }
        return null;
    }

    virtual public void OnControllerDisabled()
    {
    }

    virtual public void OnControllerEnabled()
    {
    }
}
