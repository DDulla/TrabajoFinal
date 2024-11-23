using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioMixer mainMixer;
    public AudioClip backgroundMusic;
    public AudioClip menuMusic;
    public List<AudioClip> sfxClips;
    private AudioSource musicSource;
    private List<AudioSource> sfxSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.outputAudioMixerGroup = mainMixer.FindMatchingGroups("Music")[0];
            musicSource.loop = true;
            if (menuMusic != null)
            {
                PlayMusic(menuMusic);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string clipName)
    {
        AudioClip clip = sfxClips.Find(s => s.name == clipName);
        if (clip != null)
        {
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = mainMixer.FindMatchingGroups("SFX")[0];
            sfxSource.clip = clip;
            sfxSource.Play();
            sfxSources.Add(sfxSource);
            Destroy(sfxSource, clip.length);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void SetVolume(string parameterName, float volume)
    {
        mainMixer.SetFloat(parameterName, Mathf.Log10(volume) * 20);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < sfxSources.Count; i++)
        {
            Destroy(sfxSources[i]);
        }
    }
}
