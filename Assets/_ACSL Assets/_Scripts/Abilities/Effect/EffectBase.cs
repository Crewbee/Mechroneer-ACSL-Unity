using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectBase
{
    protected IEffectUser m_affectedObject { get; private set; }
    protected GameObject m_gameObject { get; private set; }
    public EffectBase(IEffectUser affectedObject)
    {
        m_affectedObject = affectedObject;
        m_gameObject = m_affectedObject.GetGameObject();
    }

    // Update is called once per frame
    public abstract void Update();
    public abstract void FixedUpdate();
    public virtual void OnObjectHit(Collider other)
    {

    }
    public virtual void OnAffectedObjectDestroyed()
    {
        m_affectedObject = null;
    }
}
