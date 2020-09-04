using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Robot"))
        //{
        //    //if (other.gameObject.CompareTag("Robot") != this.gameObject)
        //    {
        //        if (other.gameObject)
        //        {
        //            GameObject target = other.gameObject;
        //            if (target)
        //            {
        //                if (target.GetComponent<HealthComponent>() != null)
        //                {
        //                    HealthComponent robotHealth = target.GetComponent<HealthComponent>();
        //                    if (this.GetComponentInParent<RobotArm>().autoAttackData != null)
        //                    {
        //                        if (this.GetComponentInParent<Animator>().GetBool("SpecialAttack"))
        //                        {
        //                            Debug.Log("SpecialAttack");
        //                            robotHealth.ApplyDamage(this.GetComponentInParent<RobotArm>().specialAbilityData.damage);
        //                        }
        //                        else if (this.GetComponentInParent<Animator>().GetBool("AutoAttack"))
        //                        {
        //                            Debug.Log("AutoAttack");
        //                            robotHealth.ApplyDamage(this.GetComponentInParent<RobotArm>().autoAttackData.damage);
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }

}
