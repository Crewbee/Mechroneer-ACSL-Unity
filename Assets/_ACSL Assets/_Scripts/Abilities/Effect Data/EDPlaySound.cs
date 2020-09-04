using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/Sound")]
public class EDPlaySound : EffectData
{
    public string sound;
    public int sourceID = 0;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        AudioSource[] sources = caller.GetCustomAudioSources(abilityData.customSpawnPointID);
        int id = sourceID;
        //Debug.Log("Activate ability - sound");
        if (sources.Length == 0)
        {
            //Debug.Log("Source Length = 0");
            return;
        }
        else if (sourceID > sources.Length)
        {
            id = 0;
        }
        AudioSource source = caller.GetCustomAudioSources(abilityData.customSpawnPointID)[id];
        if (source)
        {
            //AudioManager.instance.StopSound();
           AudioManager.instance.PlaySoundAtPoint(sound, source.transform.position);
        }
        else
        {
            Debug.Log("Source failed");
        }
    }
}
