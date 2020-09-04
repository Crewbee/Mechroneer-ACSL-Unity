//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

/// TODO: DO NOT DELETE

//public class IonBeam : SpecialAttack
//{
//    private GameObject m_Spawner;
//    private Transform m_Muzzle;
//    private Transform m_Target;
//    private RobotArm m_Arm;

//    public bool m_Firing = false;

//    private MyTimer m_DelayUntilNextShot = new MyTimer();

//    // Start is called before the first frame update
//    public void Start(GameObject owner)
//    {
//        Owner = owner;
//        m_Arm = Owner.GetComponent<RobotArm>();

//        m_Spawner = Owner.transform.parent.Find("LaserSpawner").gameObject;
//        m_Spawner.transform.SetParent(Owner.transform.parent);

//        m_Cooldown = new MyTimer();
//        CalculateDamage();
//    }
//    override public void CalculateDamage()
//    {
//        m_DamageType = DamageType.Electric;
//        m_SpecialDamage = 220.0f;
//    }
//    // Update is called once per frame
//    override public void Update()
//    {
//        if ((!m_Cooldown.active && m_IsActive == true && Owner.GetComponentInParent<EnergyComponent>().energy >= 75.0f))
//        {
//            Owner.GetComponentInParent<EnergyComponent>().UseEnergy(75);
//            m_Cooldown.StartTimer(m_CooldownTime);

//            m_Target = m_Arm.target;
//            m_Muzzle = Owner.transform.Find("Muzzle");

//            m_Spawner.GetComponent<LaserSpawner>().SpawnLaser(SpawnableTypes.ST_IONBEAM, m_Target.position, m_Muzzle.position, m_SpecialDamage);

//            HealthComponent tempHealthComponent = m_Target.parent.GetComponent<HealthComponent>();
//            if (tempHealthComponent)
//            {
//                tempHealthComponent.ApplyDamage(m_SpecialDamage);
//            }
//        }
//        else
//        {
//            m_IsActive = false;
//        }

//        m_Cooldown.Update();
//    }
//}
