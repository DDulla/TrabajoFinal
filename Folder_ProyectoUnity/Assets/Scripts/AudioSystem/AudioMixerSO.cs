using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioMixerSO", menuName = "Scriptable Objects/Audio/AudioMixerSO", order = 2)]
public class AudioMixerSO : ScriptableObject {
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private AudioMixerGroup groupMixer;
    [SerializeField] private string volumeKey;
    [Range(1e-08f,1)]
    [SerializeField] private float volumeValue;

    [HideInInspector] public bool IsMuted{
        get{return PlayerPrefs.GetFloat(volumeKey,0.5f) == 1e-08f;}
        set{}
    }

    private void OnEnable() {
        volumeValue = PlayerPrefs.GetFloat(volumeKey,0.5f);
    }

    private void OnDisable() {
        #if UNITY_EDITOR
        //Debug.Log($"ON ENABLE MIXER: {name} - Volume: {volumeValue}");
        #endif
    }

    public void EnableSound(){
        if(mainMixer != null){

            #if UNITY_EDITOR
            //Debug.Log($"ON ENABLE MIXER: {name} - Volume: {volumeValue}");
            #endif
            
            UpdateVolume(volumeValue);
        }
    }

    public float GetCurrentVolumeValue(){
        return PlayerPrefs.GetFloat(volumeKey,0.5f);
    }

    public void UpdateVolume(float value){
        mainMixer.SetFloat(volumeKey, Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat(volumeKey, value);
    }

    public AudioSource PlayOneShoot(AudioSource audioSourceParam = null, AudioClip audioClip = null){
        if(audioClip == null){
            Debug.LogWarning($"Missing sound clip for {name}");
            return null;
        }

        AudioSource source = audioSourceParam;
        if(source == null){
            var _obj = new GameObject($"Sound_{audioClip.name}", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        source.clip = audioClip;
        source.outputAudioMixerGroup = groupMixer;

        source.Play();

        Destroy(source.gameObject, source.clip.length);

        return source;
    }

    public AudioSource PlayOneShoot(AudioSource audioSourceParam = null, AudioClipSO audioClip = null){
        if(audioClip.AudioClip == null){
            Debug.LogWarning($"Missing sound clip for {name}");
            return null;
        }

        AudioSource source = audioSourceParam;
        if(source == null){
            var _obj = new GameObject($"Sound_{audioClip.AudioClip.name}", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        source.clip = audioClip.AudioClip;
        source.outputAudioMixerGroup = groupMixer;
        source.volume = audioClip.Volume;
        source.pitch = audioClip.Pitch;

        source.Play();

        Destroy(source.gameObject, source.clip.length);

        return source;
    }

    public AudioSource PlayLoop(AudioSource audioSourceParam = null, AudioClip audioClip = null){
        if(audioClip == null){
            Debug.LogWarning($"Missing sound clip for {name}");
            return null;
        }

        AudioSource source = audioSourceParam;
        if(source == null){
            var _obj = new GameObject($"Sound_{audioClip.name}", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        source.outputAudioMixerGroup = groupMixer;

        source.clip = audioClip;
        source.loop = true;
        source.Play();

        return source;
    }

    public AudioSource PlayLoop(AudioSource audioSourceParam = null, AudioClipSO audioClip = null){
        if(audioClip.AudioClip == null){
            Debug.LogWarning($"Missing sound clip for {name}");
            return null;
        }

        AudioSource source = audioSourceParam;
        if(source == null){
            var _obj = new GameObject($"Sound_{audioClip.AudioClip.name}", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        source.clip = audioClip.AudioClip;
        source.outputAudioMixerGroup = groupMixer;
        source.volume = audioClip.Volume;
        source.pitch = audioClip.Pitch;
        source.loop = true;

        source.Play();

        return source;
    }
}