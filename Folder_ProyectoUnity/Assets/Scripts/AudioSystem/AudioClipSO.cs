using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipSO", menuName = "Scriptable Objects/Audio/AudioClipSO", order = 1)]
public class AudioClipSO : ScriptableObject {
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioMixerSO mixerSO;
    [Range(0,1), SerializeField] private float volume = 1f;
    [Range(-3,3), SerializeField] private float pitch = 1f;

    public AudioClip AudioClip{
        get{return audioClip;}
        set{}
    }

    public float Volume{
        get{return volume;}
        set{}
    }

    public float Pitch{
        get{return pitch;}
        set{}
    }

    public void PlayOneShoot(){
        mixerSO.PlayOneShoot(null,this);
    }

    public void PlayLoop(){
        mixerSO.PlayLoop(null,this);
    }
}