using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public ScreenTransition screenTransition;
    public TitleAnimator titleAnimator; 

    private void Start()
    {
        float masterVolume, musicVolume, sfxVolume;
        AudioManager.Instance.mainMixer.GetFloat("MasterVolume", out masterVolume);
        AudioManager.Instance.mainMixer.GetFloat("MusicVolume", out musicVolume);
        AudioManager.Instance.mainMixer.GetFloat("SFXVolume", out sfxVolume);
        masterSlider.value = Mathf.Pow(10, masterVolume / 20);
        musicSlider.value = Mathf.Pow(10, musicVolume / 20);
        sfxSlider.value = Mathf.Pow(10, sfxVolume / 20);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        if (AudioManager.Instance == null)
        {
            GameObject audioManager = new GameObject("AudioManager");
            audioManager.AddComponent<AudioManager>();
        }

        screenTransition.InitializeTransition();
    }

    public void StartGame()
    {
        screenTransition.StartGame();
        AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
    }
    public void StartProto()
    {
        screenTransition.StartProto();
        AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
    }
    public void GBToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMasterVolume(float volume)
    {
        AudioManager.Instance.SetVolume("MasterVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetVolume("MusicVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetVolume("SFXVolume", Mathf.Lerp(-80, 0, volume));
    }
}
