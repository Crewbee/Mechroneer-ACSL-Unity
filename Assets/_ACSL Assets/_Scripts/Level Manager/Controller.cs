using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Controller.IActions possessedPlayer { get; private set; }
    public bool controllerEnabled { get; private set; }

    protected GameObject m_cachedGameObject;

    private delegate void UpdateFunction();
    UpdateFunction m_updateFunction;
    UpdateFunction m_fixedUpdateFunction;

    private void Awake()
    {
        controllerEnabled = false;
        possessedPlayer = null;
        m_updateFunction = DefaultUpdate;
        m_fixedUpdateFunction = DefaultUpdate;

    }
    virtual public void PossessPlayer(Controller.IActions actions)
    {
        if (possessedPlayer == actions)
            return;
        possessedPlayer = actions;
        if (actions != null)
            m_cachedGameObject = actions.GetGameObject();
        SetControllerEnabled(actions != null);
    }

    // Update is called once per frame
    void Update()
    {
        m_updateFunction();
    }
    private void FixedUpdate()
    {
        m_fixedUpdateFunction();
    }

    void DefaultUpdate() { }
    protected virtual void UpdateController() { }
    protected virtual void FixedUpdateController() { }

    public void SetControllerEnabled(bool enabled)
    {
        if (controllerEnabled == enabled)
            return;

        controllerEnabled = enabled;
        if (controllerEnabled)
        {
            m_updateFunction = UpdateController;
            m_fixedUpdateFunction = FixedUpdateController;
            if (possessedPlayer != null)
                possessedPlayer.OnControllerEnabled();
        }
        else
        {
            m_updateFunction = DefaultUpdate;
            m_fixedUpdateFunction = DefaultUpdate;
            if (possessedPlayer != null)
                possessedPlayer.OnControllerDisabled();
        }
    }

    public interface IActions
    {
        GameObject GetGameObject();
        void OnControllerEnabled();
        void OnControllerDisabled();
    }
}
