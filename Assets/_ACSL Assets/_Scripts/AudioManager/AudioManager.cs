using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject soundObject;
    public List<SoundCollection> soundCollections;

    public Sound lastSound { private set; get; } = null;

    // Singleton
    public static AudioManager instance;

    private void Awake()
    {
        // Singleton check
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize each sound
        StartCoroutine(ILoadSounds());
    }

    private IEnumerator ILoadSounds()
    {
        // Initialize each sound
        foreach (SoundCollection collection in soundCollections)
        {
            foreach (Sound sound in collection.sounds)
            {
                AudioSource source = sound.source;
                if (!source)
                {
                    sound.source = gameObject.AddComponent<AudioSource>();
                    source = sound.source;
                }
                sound.source.clip = sound.clip;
                sound.source.outputAudioMixerGroup = sound.output;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.playOnAwake = sound.playOnAwake;
                if (sound.source.playOnAwake)
                {
                    sound.source.Play();
                }
            }
        }

        yield return null;
    }

    public Sound GetWeightedSound(SoundCollection collection)
    {
        float totalWeight = 0;
        for (int i = 0; i < collection.sounds.Count; i++)
            totalWeight += collection.sounds[i].weight;

        float random = Random.Range(0, totalWeight);
        int index = 0;
        for(int i = 0; i< collection.sounds.Count; i++)
        {
            index += Mathf.RoundToInt(collection.sounds[i].weight);
            if (random < index)
                return collection.sounds[i];
        }

        return null;
    }

    public Sound GetSound(string name)
    {
        foreach (SoundCollection collection in soundCollections)
        {
            Sound sound = collection.sounds.Find(x => x.name == name);
            if (sound != null)
                return sound;
            //if (sound == null)
            //{
            //    Debug.LogError("Sound: " + name + " not found!");
            //    //Debug.Break();
            //    continue;
            //}

            //lastSound = sound;
            //return sound;
        }

        return null;
    }

    public AudioSource GetSource(string name)
    {
        return GetSound(name).source;
    }

    public Sound GetRandomSound()
    {
        SoundCollection collection = soundCollections[Random.Range(0, soundCollections.Count)];
        Sound sound = new Sound();
        while (sound == lastSound)
         sound = GetWeightedSound(collection);

        if (sound == null)
        {
            Debug.LogError("Sound Invalid!");
            Debug.Break();
            return null;
        }

        lastSound = sound;
        return sound;
    }

    public Sound GetRandomSoundIn(SoundCollection collection)
    {
        Sound sound = new Sound();
        while (sound == lastSound)
            sound = GetWeightedSound(collection);

        if (sound == null)
        {
            Debug.LogError("Sound Invalid!");
            Debug.Break();
            return null;
        }

        lastSound = sound;
        return sound;
    }

    public void PlaySound(string name)
    {
        Sound sound = GetSound(name);
        sound.source.Play();
    }

    public void PlaySoundFrom(string name, AudioSource source)
    {
        source.clip = GetSound(name).clip;
        source.loop = false;
        source.Play();
    }

    public void PlaySoundAtPoint(string name, Vector3 position)
    {
        Sound sound = GetSound(name);
        AudioSource.PlayClipAtPoint(sound.clip, position);
    }

    public void PlaySoundAtTime(string name, float time)
    {
        Sound sound = GetSound(name);
        sound.source.time = time;
        sound.source.Play();
    }

    public void PlayRandomSound()
    {
        Sound sound = GetRandomSound();
        sound.source.Play();
    }

    public void PlayRandomSoundIn(SoundCollection collection)
    {
        Sound sound = GetRandomSoundIn(collection);
        sound.source.Play();
    }

    public void PauseSound(string name)
    {
        Sound sound = GetSound(name);
        sound.source.Pause();
    }

    public void UnPauseSound(string name)
    {
        Sound sound = GetSound(name);
        sound.source.UnPause();
    }

    public void StopSound(string name)
    {
        Sound sound = GetSound(name);
        sound.source.Stop();
    }

    public void Fade(string name, float volume, float duration)
    {
        StartCoroutine(IFade(name, volume, duration));
    }

    public void FadeOut(string name, float duration)
    {
        StartCoroutine(IFade(name, 0, duration));
    }

    public void FadeIn(string name, float duration)
    {
        StartCoroutine(IFade(name, 1, duration));
    }

    private IEnumerator IFade(string name, float volume, float duration)
    {
        Sound sound = GetSound(name);

        if (sound == null)
        {
            yield return null;
        }

        while (sound.source.volume != volume)
        {
            if (sound.source.volume < volume)
            {
                sound.source.volume += Time.deltaTime / duration;
            }
            else if (sound.source.volume > volume)
            {
                sound.source.volume -= Time.deltaTime / duration;
            }

            yield return null;
        }

        //if (sound.source.volume == volume && !sound.source.loop)
        //{
        //    sound.source.Stop();
        //}

        if (sound.source.volume == 0)
        {
            sound.source.Stop();
        }

        yield return new WaitForSeconds(duration);
    }
}
