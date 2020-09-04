//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//TODO: Delete this script
//public class WeaponBehaviors : MonoBehaviour
//{
//    public Collider collider;
//    // Ranged ///////

//    //MiniGun
//    public void MiniGunBehavior(AbilityData data, Transform origin, Transform target)
//    {
//        if (target != null)
//        {
//            Vector3 Dir = target.position - origin.position;
//            Dir.Normalize();

//            float FinalDamage = data.damage;

//            ObjectPooler.Instance.SpawnFromPool(SpawnableTypes.ST_BULLET, origin.position, Quaternion.identity, Dir, data.damage, origin.parent, target);
//        }
//    }

//    //Laser
//    public void LaserBehavior(AbilityData data, Transform origin, Transform target)
//    {
//        Vector3 HitPosition = Vector3.zero;
//        RaycastHit hit;

//        Vector3 Direction = target.position - origin.position;
//        Direction = Direction.normalized;

//        Ray ray = new Ray(origin.position, Direction);

//        if (Physics.Raycast(ray, out hit))
//        {
//            if (hit.collider != null)
//            {
//                GameObject targ = hit.collider.gameObject;

//                if (targ.tag == "Robot")
//                {
//                    HitPosition = hit.point;

//                    ObjectPooler.Instance.SpawnLaserFromPool(SpawnableTypes.ST_LASER, HitPosition, origin.position, data.damage, origin.parent);

//                    if (target.GetComponent<HealthComponent>() != null)
//                    {
//                        HealthComponent robotHealth = target.GetComponent<HealthComponent>();
//                        robotHealth.ApplyDamage(data.damage);
//                    }
//                    else if (target.parent)
//                    {
//                        if (target.parent.GetComponent<HealthComponent>() != null)
//                        {
//                            HealthComponent robotHealth = target.parent.GetComponent<HealthComponent>();
//                            robotHealth.ApplyDamage(data.damage);
//                        }
//                    }
//                }
//            }

//        }
//    }

//    //Clusterbomb
//    public void ClusterBombBehavior(AbilityData data, Transform origin, Transform target)
//    {

//        Vector3 Dir = target.position - origin.position;
//        float speed = 0.0f;

//        speed = Dir.magnitude;
//        if (speed < 200.0f)
//        {
//            speed = 200.0f;
//        }

//        Dir.y = Dir.magnitude * 0.2f;
//        Dir.Normalize();

//        float FinalDamage = data.damage;

//        Vector3 spawnPoint = origin.position + (origin.forward * 1.2f);

//        ObjectPooler.Instance.SpawnFromPool(SpawnableTypes.ST_CLUSTERBOMB, spawnPoint, Quaternion.identity, Dir * speed, FinalDamage, origin.parent, target);

//    }

//    //IonBeam
//    public void IonBeamBehavior(AbilityData data, Transform origin, Transform target)
//    {
//        Vector3 HitPosition = Vector3.zero;
//        RaycastHit hit;

//        Vector3 Direction = target.position - origin.position;
//        Direction = Direction.normalized;

//        Ray ray = new Ray(origin.position, Direction);

//        if (Physics.Raycast(ray, out hit))
//        {
//            if (hit.collider != null)
//            {
//                GameObject targ = hit.collider.gameObject;

//                if (targ.tag == "Robot")
//                {
//                    HitPosition = hit.point;

//                    ObjectPooler.Instance.SpawnLaserFromPool(SpawnableTypes.ST_IONBEAM, HitPosition, origin.position, data.damage, origin.parent);

//                    if (target.parent)
//                    {
//                        if (target.parent.GetComponent<HealthComponent>() != null)
//                        {
//                            HealthComponent robotHealth = target.parent.GetComponent<HealthComponent>();
//                            robotHealth.ApplyDamage(data.damage);
//                        }
//                    }
//                    else
//                    {
//                        if (target.GetComponent<HealthComponent>() != null)
//                        {
//                            HealthComponent robotHealth = target.GetComponent<HealthComponent>();
//                            robotHealth.ApplyDamage(data.damage);
//                        }
//                    }

//                }
//            }

//        }

//    }

//    // Melee //////
//    private void OnTriggerEnter(Collider other)
//    {
//        //deal damage
//        Debug.Log("fuck");
//    }

//    //ChainSaw
//    public void ChainSawBehavior(AbilityData data, Transform origin, Transform target)
//    {

//        if (collider)
//        {
//            GetComponent<Animator>().SetBool("AutoAttack", true);
//            GetComponent<Animator>().SetBool("SpecialAttack", false);
//        }
//        //Vector3 HitPosition = Vector3.zero;

//        //Vector3 Direction = target.position - origin.position;
//        //Direction = Direction.normalized;

//        //Ray ray = new Ray(origin.position, Direction);

//        //if (Physics.Raycast(ray, out hit))
//        //{
//        //    if (hit.collider != null)
//        //    {
//        //        GameObject targ = hit.collider.gameObject;

//        //        if (targ.tag == "Robot")
//        //        {
//        //            HitPosition = hit.point;

//        //            ObjectPooler.Instance.SpawnLaserFromPool(SpawnableTypes.ST_IONBEAM, HitPosition, origin.position, data.damage, origin.parent);

//        //            if (target.parent)
//        //            {
//        //                if (target.parent.GetComponent<HealthComponent>() != null)
//        //                {
//        //                    HealthComponent robotHealth = target.parent.GetComponent<HealthComponent>();
//        //                    robotHealth.ApplyDamage(data.damage);
//        //                }
//        //            }
//        //            else
//        //            {
//        //                if (target.GetComponent<HealthComponent>() != null)
//        //                {
//        //                    HealthComponent robotHealth = target.GetComponent<HealthComponent>();
//        //                    robotHealth.ApplyDamage(data.damage);
//        //                }
//        //            }

//        //        }
//        //    }

//        //}

//    }

//    //ChainSaw Special Slash
//    public void ChainSawSuperSlashBehavior(AbilityData data, Transform origin, Transform target)
//    {

//        if (collider)
//        {
//            GetComponent<Animator>().SetBool("SpecialAttack", true);
//            //collider.GetComponent<Collider>().isTrigger
//        }
//        //Vector3 HitPosition = Vector3.zero;

//        //Vector3 Direction = target.position - origin.position;
//        //Direction = Direction.normalized;

//        //Ray ray = new Ray(origin.position, Direction);

//        //if (Physics.Raycast(ray, out hit))
//        //{
//        //    if (hit.collider != null)
//        //    {
//        //        GameObject targ = hit.collider.gameObject;

//        //        if (targ.tag == "Robot")
//        //        {
//        //            HitPosition = hit.point;

//        //            ObjectPooler.Instance.SpawnLaserFromPool(SpawnableTypes.ST_IONBEAM, HitPosition, origin.position, data.damage, origin.parent);

//        //            if (target.parent)
//        //            {
//        //                if (target.parent.GetComponent<HealthComponent>() != null)
//        //                {
//        //                    HealthComponent robotHealth = target.parent.GetComponent<HealthComponent>();
//        //                    robotHealth.ApplyDamage(data.damage);
//        //                }
//        //            }
//        //            else
//        //            {
//        //                if (target.GetComponent<HealthComponent>() != null)
//        //                {
//        //                    HealthComponent robotHealth = target.GetComponent<HealthComponent>();
//        //                    robotHealth.ApplyDamage(data.damage);
//        //                }
//        //            }

//        //        }
//        //    }

//        //}

//    }

//    //Hammer
//    public void HammerAutoBehavior(AbilityData data, Transform origin, Transform target)
//    {

//        if(collider)
//        {
//            GetComponent<Animator>().SetBool("AutoAttack", true);
//            GetComponent<Animator>().SetBool("SpecialAttack", false);
//        }
//        //Vector3 HitPosition = Vector3.zero;

//        //Vector3 Direction = target.position - origin.position;
//        //Direction = Direction.normalized;

//        //Ray ray = new Ray(origin.position, Direction);

//        //if (Physics.Raycast(ray, out hit))
//        //{
//        //    if (hit.collider != null)
//        //    {
//        //        GameObject targ = hit.collider.gameObject;

//        //        if (targ.tag == "Robot")
//        //        {
//        //            HitPosition = hit.point;

//        //            ObjectPooler.Instance.SpawnLaserFromPool(SpawnableTypes.ST_IONBEAM, HitPosition, origin.position, data.damage, origin.parent);

//        //            if (target.parent)
//        //            {
//        //                if (target.parent.GetComponent<HealthComponent>() != null)
//        //                {
//        //                    HealthComponent robotHealth = target.parent.GetComponent<HealthComponent>();
//        //                    robotHealth.ApplyDamage(data.damage);
//        //                }
//        //            }
//        //            else
//        //            {
//        //                if (target.GetComponent<HealthComponent>() != null)
//        //                {
//        //                    HealthComponent robotHealth = target.GetComponent<HealthComponent>();
//        //                    robotHealth.ApplyDamage(data.damage);
//        //                }
//        //            }

//        //        }
//        //    }

//        //}

//    }
//    public void HammerHydroSlamBehavior(AbilityData data, Transform origin, Transform target)
//    {

//        if (collider)
//        {
//            GetComponent<Animator>().SetBool("SpecialAttack", true);
//        }
//        //Vector3 HitPosition = Vector3.zero;

//        //Vector3 Direction = target.position - origin.position;
//        //Direction = Direction.normalized;

//        //Ray ray = new Ray(origin.position, Direction);

//        //if (Physics.Raycast(ray, out hit))
//        //{
//        //    if (hit.collider != null)
//        //    {
//        //        GameObject targ = hit.collider.gameObject;

//        //        if (targ.tag == "Robot")
//        //        {
//        //            HitPosition = hit.point;

//        //            ObjectPooler.Instance.SpawnLaserFromPool(SpawnableTypes.ST_IONBEAM, HitPosition, origin.position, data.damage, origin.parent);

//        //            if (target.parent)
//        //            {
//        //                if (target.parent.GetComponent<HealthComponent>() != null)
//        //                {
//        //                    HealthComponent robotHealth = target.parent.GetComponent<HealthComponent>();
//        //                    robotHealth.ApplyDamage(data.damage);
//        //                }
//        //            }
//        //            else
//        //            {
//        //                if (target.GetComponent<HealthComponent>() != null)
//        //                {
//        //                    HealthComponent robotHealth = target.GetComponent<HealthComponent>();
//        //                    robotHealth.ApplyDamage(data.damage);
//        //                }
//        //            }

//        //        }
//        //    }

//        //}

//    }
//}
