using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: DO NOT DELETE
//public class Dash : Ability, ISpecialAbility
//{
//    public float m_DashPower = 650.0f;
//    private GameObject m_Target;

//    override public void Start()
//    {
//        m_Cooldown = new MyTimer();
//    }

//    override public void Update()
//    {
//        m_Cooldown.Update();
//    }

//    public override void SetTarget(GameObject target)
//    {
//        m_Target = target;
//    }

//    public override void ActivateAbility()
//    {
//        Rigidbody rb = m_Target.GetComponent<Rigidbody>();

//        if (rb && !m_Cooldown.active && m_Target.GetComponentInParent<EnergyComponent>().energy >= m_Energy)
//        {
//            Vector3 diff = m_Target.GetComponent<Robot>().finalPosition - m_Target.transform.position;
//            diff.y = 0;
//            diff = diff.normalized;
//            rb.AddForce(diff * m_DashPower, ForceMode.Impulse);

//            m_Target.GetComponent<Robot>().ResetPath();

//            m_Target.GetComponentInParent<EnergyComponent>().UseEnergy(m_Energy);
//            m_Cooldown.StartTimer(m_CooldownTime);

//        }
//        else if (!rb)
//        {
//            Debug.Log("No Rigidbody detected!");
//        }
//    }
//}
