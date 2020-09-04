using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class RobotListener : MonoBehaviour
{
    public TutorialRobot robot;
    public abstract event UnityAction requirementsMet;

    protected virtual void CheckConditions() { }
}
