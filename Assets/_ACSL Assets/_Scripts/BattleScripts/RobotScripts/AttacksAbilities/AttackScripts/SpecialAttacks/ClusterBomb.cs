//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


/// TODO: DO NOT DELETE

//public class ClusterBomb : SpecialAttack
//{
//    private GameObject m_Spawner;
//    private Transform m_Muzzle;
//    private Transform m_Target;
//    private RobotArm m_Arm;

//    public float m_Delay = 0.33f;
//    public int m_numOfBombs = 3;
//    public float m_BaseSpeed = 200.0f;
//    public bool m_Firing = false;


//    private int m_remainingBombs = 3;
//    private MyTimer m_DelayUntilNextShot = new MyTimer();

//    public void Start(GameObject owner)
//    {
//        Owner = owner;
//        m_Arm = Owner.GetComponent<RobotArm>();

//        m_Spawner = Owner.transform.parent.Find("ClusterBombSpawner").gameObject;
//        m_Spawner.transform.SetParent(Owner.transform.parent);
//        m_Cooldown = new MyTimer();
//        CalculateDamage();
//    }

//    override public void CalculateDamage()
//    {
//        m_DamageType = DamageType.Expolsive;
//        m_SpecialDamage = 90.0f;
//    }

//    override public void Update()
//    {
//        if ((!m_Cooldown.active && m_IsActive == true && Owner.GetComponentInParent<EnergyComponent>().energy >= 75.0f) && m_Firing == false)
//        {
//            Owner.GetComponentInParent<EnergyComponent>().UseEnergy(75);
//            m_Cooldown.StartTimer(m_CooldownTime);
//            m_Firing = true;
//        }
//        else
//        {
//            m_IsActive = false;
//        }

//        if (m_Firing == true)
//        {
//            m_DelayUntilNextShot.Update();
//            if (!m_DelayUntilNextShot.active)
//            {
//                m_Target = m_Arm.target;
//                m_Muzzle = m_Arm.muzzle;

//                if (m_Target != null)
//                {
//                    float m_Speed;
//                    Vector3 Dir = m_Target.position - m_Muzzle.position;
//                    m_Speed = Dir.magnitude * 6.0f;
//                    if (m_Speed < 200.0f)
//                    {
//                        m_Speed = m_BaseSpeed;
//                    }
//                    Dir.y = Dir.magnitude * 0.2f;
//                    Dir.Normalize();

//                    if (m_remainingBombs > 0)
//                    {
//                        m_Spawner.GetComponent<ClusterBombSpawner>().SpawnClusterBomb(m_Muzzle.position + (m_Muzzle.transform.forward * 1.2f), m_Muzzle.transform.forward, m_SpecialDamage, m_Speed);
//                        m_DelayUntilNextShot.StartTimer(m_Delay);
//                        m_remainingBombs--;
//                    }
//                    else
//                    {
//                        m_DelayUntilNextShot.StartTimer(m_Delay);
//                        m_remainingBombs = m_numOfBombs;
//                        m_IsActive = false;
//                        m_Firing = false;
//                    }
//                }
//            }
//        }
//        m_Cooldown.Update();
//    }
//}
