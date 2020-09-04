using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : Task
{
    // Use this for initialization
    public float TurnSpeed = 20.0f;
    private float m_Damage;

    public AutoAttack(float attackTime, float damage)
    {
        executionTime = attackTime;
        m_Damage = damage;

        m_Index = 0;
    }
    public override NodeResult Execute()
    {
        GameObject go = tree.gameObject;
        GameObject Target = (GameObject)tree.GetValue("Target");
        if (Target != null)
        {
            Vector3 target = Target.transform.position;
            elapsedTime += Time.fixedDeltaTime;

            if (elapsedTime >= executionTime)
            {
                InflictDamage();
                return NodeResult.SUCCESS;
            }

            Vector3 position = new Vector3(go.transform.position.x, -target.y, go.transform.position.z);
            Vector3 direction = target - position;
            direction.y = 0;

            go.transform.rotation = Quaternion.Slerp(go.transform.rotation, Quaternion.LookRotation(direction), TurnSpeed * Time.deltaTime);

            //if (Vector3.Distance(go.transform.position, target.transform.position) < Speed * Time.deltaTime)
            //{
            //    go.transform.position = target.transform.position;
            //}
            //else
            //{
            //    go.transform.Translate(0, 0, Speed * Time.deltaTime);
            //}
            //InflictDamage();
        }
        return NodeResult.RUNNING;
    }

    public void InflictDamage()
    {
        //Debug.Log("Damage: " + m_Damage);
    }

    public override void Reset()
    {
        elapsedTime = 0.0f;
        base.Reset();
    }
}
