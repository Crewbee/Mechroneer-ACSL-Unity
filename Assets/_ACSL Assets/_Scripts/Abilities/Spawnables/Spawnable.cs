using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallerData
{
    public IEffectUser caller;
    public IEffectUser target;
    public Vector3 mousePos;
    public SomethingAbility ability;
}


public abstract class Spawnable : MonoBehaviour, IEffectUser
{
    #region Base Class Variables
    protected CallerData m_callerData { get; private set; }
    protected EDSpawnable m_spawnableData { private set; get; }

    protected List<EffectBase> m_activeEffects;
    protected List<EffectBase> m_effectsToRemove;

    private MyTimer m_lifeTimeTimer = new MyTimer();
    private MyTimer m_triggerStayTimer = new MyTimer();
    private bool m_destroyCalled;

    protected List<IEffectUser> m_stayTargets;
    protected List<GameObject> m_stayTargetsGameObject;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        tag = "Spawnable";
    } //set the tag to spawnable

    // Update is called once per frame
    protected virtual void Update()
    {
        if (m_effectsToRemove != null) //if the list of effect to remove is not null
        {
            #region Remove The Effect From The Active List
            if (m_effectsToRemove.Count > 0)
            {
                foreach (EffectBase effect in m_effectsToRemove)
                    m_activeEffects.Remove(effect);
                m_effectsToRemove.Clear();
            }
            #endregion
        }
        if (m_activeEffects != null) //if the list of active effects is not null
        {
            #region Call Update Each Active Effect
            foreach (EffectBase effect in m_activeEffects)
                effect.Update();
            #endregion
        }

        m_lifeTimeTimer.Update(); //update lifetime
        m_triggerStayTimer.Update(); //update trigger timer
    }

    protected virtual void FixedUpdate()
    {
        if (m_activeEffects != null) //if the list of active effects is not null
        {
            #region Call FixedUpdate Each Active Effect
            foreach (EffectBase effect in m_activeEffects)
                effect.FixedUpdate();
            #endregion
        }
    }

    virtual public void OnSpawned(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData, EDSpawnable baseData)
    {
        #region Initialize Variables
        m_destroyCalled = false;
        if (m_callerData == null)
            m_callerData = new CallerData();
        if (m_callerData.ability == null)
            m_callerData.ability = new SomethingAbility();

        m_callerData.caller = caller;
        m_callerData.target = target;
        m_callerData.mousePos = mousePos;

        m_callerData.ability.mask = abilityData.mask;
        m_callerData.ability.abilityRange = abilityData.abilityRange;
        m_callerData.ability.style = (baseData.overrideTargetingStyle) ? baseData.overridenStyle : abilityData.style;
        m_callerData.ability.direction = abilityData.direction;
        m_callerData.ability.inputRange = abilityData.inputRange;
        m_callerData.ability.customSpawnPointID = abilityData.customSpawnPointID;

        m_spawnableData = baseData;

        if (m_lifeTimeTimer == null)
            m_lifeTimeTimer = new MyTimer();
        if (m_triggerStayTimer == null)
            m_triggerStayTimer = new MyTimer();
        if (m_activeEffects == null)
            m_activeEffects = new List<EffectBase>();
        if (m_effectsToRemove == null)
            m_effectsToRemove = new List<EffectBase>();

        if (m_stayTargets == null)
            m_stayTargets = new List<IEffectUser>();
        if (m_stayTargetsGameObject == null)
            m_stayTargetsGameObject = new List<GameObject>();

        if (baseData.lifeTime > 0)
        {
            m_lifeTimeTimer.StartTimer(baseData.lifeTime, DespawnObject);
        }

        if (baseData.followTarget)
        {
            if (m_callerData.ability.style == TargetingStyle.Self)
                transform.parent = caller.GetOwner().GetGameObject().transform;
            else if (m_callerData.ability.style == TargetingStyle.Targeted)
                transform.parent = target.GetGameObject().transform;
        }
        #endregion


        foreach (EffectData effect in baseData.onObjectSpawned)// for each effect activated on spawn
        {
            #region Activate The Effect
            effect.ActivateEffect(this, m_callerData.target, m_callerData.mousePos, m_callerData.ability);
            #endregion
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_activeEffects != null) //if the list of active effects is not null
        {
            #region Call Each Effects On Object Hit
            foreach (EffectBase effect in m_activeEffects)
            {
                effect.OnObjectHit(other);
            }
            #endregion
        }

        IEffectUser target = other.GetComponent<IEffectUser>();

        if (target == null) //if target is null
        {
            #region Despawn Object If Parameters Are Still Met
            if (other.gameObject.GetComponentInParent<IEffectUser>() != null)
                return;
            if (m_spawnableData.destroyOnCollision)
            {
                if (m_spawnableData.collisionTags.Count == 0)
                    DespawnObject();
                else
                {
                    foreach (string tag in m_spawnableData.collisionTags)
                    {
                        if (other.gameObject.tag == tag)
                        {
                            DespawnObject();
                            break;
                        }
                    }
                }
            }

            return;
            #endregion
        }

        #region If Target Is A Spawnable Exit
        if (target.GetGameObject().tag == "Spawnable")
            return;
        #endregion

        #region If the Target Is The Owner Exit
        if ((AbilityData.TargetMaskEqual(m_callerData.ability.mask, TargetMask.Self) == false) && target == m_callerData.caller.GetOwner())
            return;
        #endregion


        if (m_callerData.ability.style == TargetingStyle.Targeted) //if the spawnable was from an auto attack
        {
            #region If The Target Is Not The Owners Target Exit
            if (target != m_callerData.target)
                return;
            #endregion

            //otherwise
            #region Activate All On Object Hit Effects
            foreach (EffectData effect in m_spawnableData.onObjectHit) //for each effect in the spawnables on hit effect list
            {
                effect.ActivateEffect(this, m_callerData.target, m_callerData.mousePos, m_callerData.ability);
            }
            #endregion

            DespawnObject();
        }
        else if (m_callerData.ability.style == TargetingStyle.Skillshot || m_callerData.ability.style == TargetingStyle.Self) //if the spawnable was from a skillshot or self targeting ability
        {
            #region If the Target Is The Owner And Not Self Targeted Exit
            if ((m_callerData.ability.mask & TargetMask.Self) != TargetMask.Self)
            {
                if (target == GetOwner())
                    return;
            }
            #endregion

            #region Activate All On Object Hit Effects
            foreach (EffectData effect in m_spawnableData.onObjectHit)
            {
                effect.ActivateEffect(this, target, m_callerData.mousePos, m_callerData.ability);
            }
            #endregion

            #region If the Spawnable Is Destroyed On Contact Destroy It
            if (m_spawnableData.destroyOnCollision)
            {
                DespawnObject();
            }
            #endregion
        }

        #region If The Spawnables Stay Target List Already Contains Target Exit
        if (m_stayTargets.Exists((IEffectUser otherUser) => { return otherUser == target; }))
            return;
        #endregion

        //otherwise
        #region Add Target To Stay Targets List
        m_stayTargets.Add(target);
        m_stayTargetsGameObject.Add(target.GetGameObject());
        #endregion
    
    }

    protected void TriggerStay()
    {
        #region Activate All On Trigger Stay Effects
        foreach (EffectData effect in m_spawnableData.onTriggerStay)
        {
            for (int i = 0; i < m_stayTargets.Count; i++)
            {
                if (m_stayTargetsGameObject[i] != null)
                    effect.ActivateEffect(this, m_stayTargets[i], m_callerData.mousePos, m_callerData.ability);
            }
        }
        #endregion

        m_triggerStayTimer.StartTimer(m_spawnableData.triggerStayTickRate, TriggerStay); //restart stay timer
    }

    private void OnTriggerExit(Collider other)
    {
        IEffectUser target = other.GetComponent<IEffectUser>();

        #region If Target Is Null Exit
        if (target == null)
            return;
        #endregion

        #region If Target Is In Stay Targets List Remove It
        int index = m_stayTargets.FindIndex((IEffectUser otherUser) => { return otherUser == target; });
        if (index >= 0)
        {
            m_stayTargets.RemoveAt(index);
            m_stayTargetsGameObject.RemoveAt(index);
        }
        #endregion
    }

    void DespawnObject()
    {
        #region If Despawn Has Already Been Called Exit
        if (m_destroyCalled)
            return;
        m_destroyCalled = true;
        #endregion

        #region Activate Each On Despawn Effect
        foreach (EffectBase effect in m_activeEffects)
        {
            effect.OnAffectedObjectDestroyed();
        }

        foreach (EffectData effect in m_spawnableData.onObjectDespawned)
        {
            effect.ActivateEffect(this, m_callerData.target, m_callerData.mousePos, m_callerData.ability);
        }
        #endregion

        Destroy(gameObject);
    }

    public IEffectUser GetCreator()
    {
        return m_callerData.caller;
    }

    public IEffectUser GetOwner()
    {
        return m_callerData.caller.GetOwner();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Transform GetCustomSpawnPoint(int spawnPointID)
    {
        return transform;
    }

    public void ApplyEffect(EffectBase effect)
    {
        m_activeEffects.Add(effect);
    }

    public List<EffectBase> GetActiveEffects()
    {
        return m_activeEffects;
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

    public void DestroyGameObject()
    {
        DespawnObject();
    }

    public GameObject GetMainTarget()
    {
        return gameObject;
    }

    public void ApplyDamage(float damage)
    {
        if (this.GetComponent<RoombaHealthComponent>() != null)
        {
            this.GetComponent<RoombaHealthComponent>().ApplyDamage(damage);
            if (this.GetComponent<RoombaHealthComponent>().health <= 0.0f)
            {
                DestroyGameObject();
            }
        }

    }

    public void SubscribeOnEffectHolderDestroyed(OnEffectHolderDestroyed function)
    {
    }

    public void UnsubscribeOnEffectHolderDestroyed(OnEffectHolderDestroyed function)
    {
    }

    public int GetTeamID()
    {
        return m_callerData.caller.GetTeamID();
    }

    public void ApplyDamageReudctionEffect(DamageReductionEffect effect)
    {
    }

    public void RemoveDamageReductionEffect(DamageReductionEffect effect)
    {
    }

    public int GetObjectID()
    {
        return int.MinValue;
    }

    public AudioSource[] GetCustomAudioSources(int customID)
    {
        return GetComponents<AudioSource>();
    }

    public bool IsTeamMode()
    {
        return GetOwner().IsTeamMode();
    }
}
