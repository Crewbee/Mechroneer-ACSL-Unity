using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

[CreateAssetMenu(fileName = "New Robot Data", menuName = "Robot Data", order = 55)]
public class RobotData : ScriptableObject
{
    public void UpdateValues()
    {
        health = energy = range = speed = 0;
        foreach (var part in GetParts())
        {
            health += part.health;
            energy += part.energy;
            if (part.GetType() == typeof(RobotArm))
                range += (part as RobotArm).autoAttackData.range.magnitude;
        }
        speed = legs.m_MovementSpeed;
    }

    [HideInInspector] public float health;
    [HideInInspector] public float energy;
    [HideInInspector] public float range;
    [HideInInspector] public float speed;

    public string RobotName = "NoNameNeddie";
    public string RobotDescription = "He's got no name!";

    public RobotHead head;


    public RobotBody body;

    public RobotArm lArm;

    public RobotArm rArm;

    public RobotLeg legs;

    public RobotPart[] GetParts()
    {
        RobotPart[] partArray = new RobotPart[5];
        partArray[0] = head;
        partArray[1] = body;
        partArray[2] = lArm;
        partArray[3] = rArm;
        partArray[4] = legs;
        return partArray;
        //List<RobotPart> partList = new List<RobotPart>();
        //partList.Add(head);
        //partList.Add(body);
        //partList.Add(lArm);
        //partList.Add(rArm);
        //partList.Add(legs);
        //return partList.ToArray();
    }
}
