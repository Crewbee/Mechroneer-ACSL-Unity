using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Robot Part", menuName = "Robot Part", order = 51)]
public class SO_RobotPart : ScriptableObject
{
    [SerializeField]
    protected enum PartType
    {
        RobotHead = 0,
        RobotArm,
        RobotBody,
        RobotLeg
    }
    [SerializeField]
    public float m_Health;
    [SerializeField]
    public float m_Armor;
    [SerializeField]
    public Animator m_Animator;
    [SerializeField]
    Collider m_Collider;
}
