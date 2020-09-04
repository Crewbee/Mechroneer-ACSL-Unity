using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnEffectHolderDestroyed();
public interface IEffectUser
{
    int GetTeamID();
    int GetObjectID();
    //For spawning effects
    IEffectUser GetCreator();
    IEffectUser GetOwner();
    Transform GetCustomSpawnPoint(int spawnPointID);

    //For applying effects
    void ApplyEffect(EffectBase effect);
    void RemoveEffect(EffectBase effect);
    List<EffectBase> GetActiveEffects();

    GameObject GetGameObject();
    GameObject GetMainTarget();
    void DestroyGameObject();

    //void SubscribeOnEffectHolderDestroyed(OnEffectHolderDestroyed function);
    //void UnsubscribeOnEffectHolderDestroyed(OnEffectHolderDestroyed function);

    bool IsTeamMode();

    void ApplyDamage(float damage);

    void ApplyDamageReudctionEffect(DamageReductionEffect effect);
    void RemoveDamageReductionEffect(DamageReductionEffect effect);

    AudioSource[] GetCustomAudioSources(int customID);

}
