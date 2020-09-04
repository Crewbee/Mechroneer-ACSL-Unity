using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLeg : RobotPart 
{
    private Vector3 previousPos;
    public float speed;
    public AudioSource audio;
    public float maxVolume;
    //public AbilityData specialAbilityData;

    public float m_MovementSpeed;

    public float m_TurningSpeed { get; set; }

    //protected override void Start()
    //{
    //    base.Start();
    //    specialAbilityTimer = new MyTimer();
    //    partType = RobotPartType.Leg;
    //}
    public override Transform GetCustomSpawnPoint()
    {
        return transform;
    }
    protected override void Update()
    {
        #region Update Timers
        specialAbilityTimer.Update();
        abilityChargesTimer.Update();
        if(previousPos == null)
        {
            previousPos = transform.position;
        }
        
        if (m_Animator)
        {
            speed = Vector3.Distance(previousPos, transform.position);
                m_Animator.SetFloat("Moving", speed);
            if (speed > maxVolume)
            {
                audio.volume = maxVolume;
            }
            else
            {
                audio.volume = speed;
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
            }
            //Debug.Log(speed);
            //m_Animator.SetBool("Moving", true);
            previousPos = transform.position;
        }

            
        #endregion
    }
}
