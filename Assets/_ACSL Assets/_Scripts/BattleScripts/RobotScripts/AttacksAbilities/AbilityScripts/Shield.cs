using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: DO NOT DELETE
//public class Shield : Ability, ISpecialAbility
//{
//    public GameObject ShieldObject { get; set; }
//    bool m_defenseUp = false;
//    private ShieldLogic logic;
//    private GameObject m_Target;

//    override public void Start()
//    {
//        m_Cooldown = new MyTimer();
//        ShieldObject.GetComponent<ShieldLogic>().PlayerReference = m_Target;
//        logic = ShieldObject.GetComponent<ShieldLogic>();
//        m_CooldownTime = logic.ShieldLifetime + 4.0f;
//    }
//    override public void Update()
//    {
//        m_Cooldown.Update();
//        if (m_Cooldown.timeLeftSeconds <= 4.0f && m_defenseUp == true)
//        {
//            m_defenseUp = false;
//            m_Target.GetComponentInParent<HealthComponent>().armor = 1.0f;
//        }
//    }

//    public override void SetTarget(GameObject target)
//    {
//        //Debug.Log("Target: " + target.name);
//        m_Target = target;

//    }

//    public override void ActivateAbility()
//    {
//            if (!m_Cooldown.active && m_Target.GetComponentInParent<EnergyComponent>().energy >= m_Energy)
//            {
//                ShieldObject.GetComponent<ShieldLogic>().PlayerReference = m_Target;
//                logic.Activate();
//                m_defenseUp = true;
//                m_Target.GetComponentInParent<HealthComponent>().armor = 0.25f;
//                m_Target.GetComponentInParent<EnergyComponent>().UseEnergy(m_Energy);

//                m_Cooldown.StartTimer(m_CooldownTime);
//            }
//    }

//}
