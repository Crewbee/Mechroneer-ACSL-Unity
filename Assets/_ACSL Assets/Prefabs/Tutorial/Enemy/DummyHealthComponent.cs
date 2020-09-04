using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealthComponent : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float armor;

    public GameObject floatingDamageSlight;
    public GameObject floatingDamageMed;
    public GameObject floatingDamageCrit;

    private int m_Damage;

    //EffectData

    // Start is called before the first frame update
    void Start()
    {
        if (floatingDamageSlight == null || floatingDamageMed == null || floatingDamageCrit == null)
        {
            Debug.LogError("Missing Damage text prefab");
        }
    }
    private void Update()
    {

    }
    public void ApplyDamage(float damage)
    { 
        m_Damage = (int)damage;

        SpawnFloatingDamageNumber();

    }
    public void SpawnFloatingDamageNumber()
    {
        if (m_Damage > 160)
        {
            GameObject go = Instantiate(floatingDamageCrit, transform.position, Quaternion.identity);
            go.GetComponent<DamageNumberScript>().UpdateFont(m_Damage);
            go.GetComponent<DamageNumberScript>().owner = transform.gameObject;
        }
        else if (m_Damage > 60)
        {
            GameObject go = Instantiate(floatingDamageMed, transform.position, Quaternion.identity);
            go.GetComponent<DamageNumberScript>().UpdateFont(m_Damage);
            go.GetComponent<DamageNumberScript>().owner = transform.gameObject;
        }
        else if (m_Damage <= 160)
        {
            GameObject go = Instantiate(floatingDamageSlight, transform.position, Quaternion.identity);
            go.GetComponent<DamageNumberScript>().UpdateFont(m_Damage);
            go.GetComponent<DamageNumberScript>().owner = transform.gameObject;
        }
    }

}
