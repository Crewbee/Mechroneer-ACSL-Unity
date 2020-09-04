using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [Header("Audio Settings")]
    public AudioClip clip;
    public AudioSource source;
    public AudioMixerGroup output;

    public string name;

    [Range(0, 1)]
    public float volume = 1f;
    [Range(-3, 3)]
    public float pitch = 1f;
    public bool loop;
    public bool playOnAwake;
    [Range(0, 1)]
    public float weight = 1f;
}