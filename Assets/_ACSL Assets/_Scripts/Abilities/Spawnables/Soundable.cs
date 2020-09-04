using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Soundable : Spawnable
{
    private List<string> soundTag;
    private AudioManager manager;

    #region Unused Fade Variables
    //private float fadeAmount;
    //private float fadeInTime;
    //private float fadeOutTime;
    #endregion

    private bool isRandomInCollection;
    private float setSoundStartTime;
    private EDSoundable soundData;
    private MyTimer startSoundTimer;
    private MyTimer soundableLifeTime;
    private bool soundHasBeenPlayed;
    private string soundToUse;

    public override void OnSpawned(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData, EDSpawnable baseData)
    {
        base.OnSpawned(caller, target, mousePos, abilityData, baseData);

        soundData = baseData as EDSoundable;
        soundTag = new List<string>();

        foreach (string Name in soundData.soundName)
        {
            soundTag.Add(Name);
        }
        manager = AudioManager.instance;

        isRandomInCollection = soundData.isRandomInCollection;
        setSoundStartTime = soundData.setSoundStartTime;


        if (setSoundStartTime > 0.0f)
        {
            startSoundTimer = new MyTimer();
            startSoundTimer.StartTimer(setSoundStartTime);
        }
        else
        {
            startSoundTimer = null;
        }
        soundHasBeenPlayed = false;
        if (soundTag.Count > 1)
        {
            int rand = (int)Random.Range(0, soundTag.Count);
            manager.GetSound(soundTag[rand]).source = this.GetComponent<AudioSource>();
            soundToUse = soundTag[rand];
            soundableLifeTime = new MyTimer();
            float lifeTime = manager.GetSound(soundTag[rand]).clip.length;
            soundableLifeTime.StartTimer(lifeTime);
        }
        else
        {
            manager.GetSound(soundTag[0]).source = this.GetComponent<AudioSource>();
            soundToUse = soundTag[0]; 
            soundableLifeTime = new MyTimer();
            float lifeTime = manager.GetSound(soundTag[0]).clip.length;
            soundableLifeTime.StartTimer(lifeTime);

        }


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        soundableLifeTime.Update();

        if (!soundHasBeenPlayed)
        {
            if (startSoundTimer != null)
            {
                startSoundTimer.Update();
                if (!startSoundTimer.active)
                {
                    manager.PlaySound(soundToUse);
                    soundHasBeenPlayed = true;
                }
            }
            else
            {
                manager.PlaySound(soundToUse);
                soundHasBeenPlayed = true;
            }
        }
        if (!soundableLifeTime.active)
        {
            DespawnObject();
        }

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void DespawnObject()
    {
        base.DestroyGameObject();
    }
}
