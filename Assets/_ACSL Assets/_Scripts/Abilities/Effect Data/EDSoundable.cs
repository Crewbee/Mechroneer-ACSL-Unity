using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/SoundableData")]
public class EDSoundable : EDSpawnable
{
    [Header("Soundable Data")]
    public Soundable objectToSpawn;
    public List<string> soundName;

    #region Unused Fade Variables
    //public float fadeAmount;
    //public float fadeInTime;
    //public float fadeOutTime;
    #endregion

    public bool isRandomInCollection;
    public float setSoundStartTime;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Spawnable objectSpawned = SpawnObject(objectToSpawn, caller, target, mousePos, abilityData);
        objectSpawned.OnSpawned(caller, target, mousePos, abilityData, this);
    }
}
