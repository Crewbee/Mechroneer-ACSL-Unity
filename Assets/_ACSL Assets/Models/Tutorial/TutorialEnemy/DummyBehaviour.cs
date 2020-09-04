using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBehaviour : MonoBehaviour, IEffectUser
{
    Animator m_dummyAnim;
    MyTimer m_downTimer;
    MyTimer m_HitTimer;

    List<EffectBase> m_activeEffects;
    List<EffectBase> m_effectsToRemove;

    DummyHealthComponent m_dummyHealth;

    GameObject robotPlayer;

    int m_numHits;
    bool m_hitRand = false;
    bool m_getUp = false;
    bool m_hit = false;
    // Start is called before the first frame update
    void Start()
    {
        m_dummyAnim = GetComponent<Animator>();
        m_downTimer = new MyTimer();
        m_HitTimer = new MyTimer();
        m_activeEffects = new List<EffectBase>();
        m_effectsToRemove = new List<EffectBase>();
        m_dummyHealth = GetComponent<DummyHealthComponent>();
        m_numHits = 0;
        m_getUp = false;
        robotPlayer = GameObject.Find("Mechroneer");
    }
    //trigger hit
    //switch between hitrand (bool)
    //if fall have timer (wait 3 secs)
    //then trigger get up
    // Update is called once per frame
    void Update()
    {
        if(robotPlayer)
        {
            transform.LookAt(robotPlayer.transform);
            
            if(m_hit == true)
            {
                ////
                //if (m_dummyAnim.GetCurrentAnimatorStateInfo(1).normalizedTime > 1)
                //    Debug.Log("not playing");
                //else
                //{
                //    Debug.Log("playing");
                //    m_hit = false;
                //    m_dummyAnim.SetBool("Hit", false);
                //}
            }
        }
        else
        {
            robotPlayer = GameObject.Find("User_Robot");
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }

    public int GetTeamID()
    {
        return -1;
    }

    public int GetObjectID()
    {
        return -1;
    }

    public IEffectUser GetCreator()
    {
        return this;
    }

    public IEffectUser GetOwner()
    {
        return this;
    }

    public Transform GetCustomSpawnPoint(int spawnPointID)
    {
       return transform;
    }

    public void ApplyEffect(EffectBase effect)
    {
        m_activeEffects.Add(effect);
    }

    public void RemoveEffect(EffectBase effect)
    {
        int findIndex = 0;
        foreach (EffectBase findEffect in m_activeEffects)
        {
            if (findEffect == effect)
                break;
            findIndex++;
        }

        if (findIndex == m_activeEffects.Count)
            return;

        m_effectsToRemove.Add(m_activeEffects[findIndex]);
    }

    public List<EffectBase> GetActiveEffects()
    {
        return m_activeEffects;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public GameObject GetMainTarget()
    {
        return gameObject;
    }

    public void DestroyGameObject()
    {
    }

    public void SubscribeOnEffectHolderDestroyed(OnEffectHolderDestroyed function)
    {
    }

    public void UnsubscribeOnEffectHolderDestroyed(OnEffectHolderDestroyed function)
    {
    }

    public void ApplyDamage(float damage)
    {
        if (m_getUp == false)
        {
            if (m_numHits > 15 || damage >= 200.0f)
            {
                m_dummyAnim.SetBool("Idle", false);
                m_dummyAnim.SetTrigger("Fall");
                m_getUp = true;
                m_numHits = 0;
                m_dummyAnim.SetInteger("Number of hits", m_numHits);
            }
            else
            {
                m_HitTimer.StartTimer(0.5f, HitFinished);
                m_numHits++;
                m_dummyAnim.SetInteger("Number of hits", m_numHits);
                m_dummyAnim.SetTrigger("Hit");
                m_dummyAnim.SetBool("Idle", true);
            }
        }
        else
        {
            m_numHits = 0;
            m_getUp = false;
            m_dummyAnim.SetBool("Hit", false);
            m_dummyAnim.SetTrigger("GetUp");
            m_dummyAnim.SetTrigger("GetUpTrigger");
            m_dummyAnim.SetBool("Idle", true);
        }

        m_dummyHealth.ApplyDamage(damage);
    }

    public void GetUp()
    {
        m_dummyAnim.SetTrigger("GetUp");
        m_downTimer.StartTimer(3.0f, GetUpFinished);
        m_getUp = false;
    }

    public void HitFinished()
    {
        m_dummyAnim.SetBool("Hit", false);
        //m_dummyAnim.SetTrigger("HitFinished");
    }

    public void GetUpFinished()
    {
        m_dummyAnim.SetTrigger("GetUpTrigger");
    }

    public void ApplyDamageReudctionEffect(DamageReductionEffect effect)
    {
    }

    public void RemoveDamageReductionEffect(DamageReductionEffect effect)
    {
 
    }

    public AudioSource[] GetCustomAudioSources(int customID)
    {
        return null;
    }

    public bool IsTeamMode()
    {
        return false;
    }
}
