using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;
    private Dictionary<string, Sound> soundsDict = new();

    public AudioSource introMusicSource;
    public AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            float volume = sound.volume;
            audioSource.volume = volume;
            sound.source = audioSource;
            soundsDict[sound.name] = sound;
        }
    }

    public void PlaySound(string name)
    {
        Sound sound = soundsDict[name];
        sound.source.PlayOneShot(sound.GetClip());
    }

    public void PlayMusic()
    {
        introMusicSource.Play();
        musicSource.PlayDelayed(introMusicSource.clip.length);
    }

    IEnumerator FadeIn(AudioSource source, float duration = 0.15f)
    {
        float targetVolume = source.volume; // Store the target volume
        source.volume = 0.0f; // Start from volume 0
        source.Play(); // Start playing the audio

        float timer = 0.0f;

        while (timer < duration)
        {
            // fix, this doesnt work for some reason
            timer += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(0.0f, targetVolume, timer / duration);
            yield return null; // Wait for the next frame
        }

        source.volume = targetVolume; // Ensure it's set to the target volume
    }

    IEnumerator FadeOut(AudioSource source, float duration = 0.15f)
    {
        float startVolume = source.volume;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(startVolume, 0.0f, timer / duration);
            yield return null; // Wait for the next frame
        }

        source.Stop();
        source.volume = 1.0f;
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        [HideInInspector] public AudioSource source;
        public AudioClip[] clips;
        private int currentClipIndex = -1;
        [Range(0.0001f, 1f)] public float volume;

        public AudioClip GetClip()
        {
            if (currentClipIndex + 1 < clips.Length)
            {
                currentClipIndex++;
            }
            else
            {
                currentClipIndex = 0;
            }

            return clips[currentClipIndex];
        }
    }
}
