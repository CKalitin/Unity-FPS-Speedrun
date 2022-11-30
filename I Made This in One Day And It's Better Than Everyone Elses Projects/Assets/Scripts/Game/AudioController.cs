using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SoundEffect {
    public AudioClip audioClip;
    public float pitchMin;
    public float pitchMax;
    public float volumeMin;
    public float volumeMax;
}

public class AudioController : MonoBehaviour {
    public static AudioController instance;

    [Header("Audio Controller")]
    [Tooltip("This is the Audio Source that will play a clip at a point.")]
    [SerializeField] private GameObject audioClipSourceGameObject;
    [SerializeField] private float audioRangeMultiplier = 3f;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource walkingSource;
    [SerializeField] private AudioSource runningSource;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(gameObject);
        }
    }

    #region Toggle Clips

    public void ToggleWalkingAudio(bool _toggle) {
        if (_toggle) walkingSource.mute = false;
        else walkingSource.mute = true;
    }

    public void ToggleRunningAudio(bool _toggle) {
        if (_toggle) runningSource.mute = false;
        else runningSource.mute = true;
    }

    #endregion

    #region Play Clips

    public void PlayerSoundEffect(SoundEffect _soundEffect) {
        GameObject audioSourceGameObject = Instantiate(audioClipSourceGameObject, Camera.main.transform.position, Quaternion.identity);
        AudioSource audioSource = audioSourceGameObject.GetComponent<AudioSource>();
        
        audioSource.pitch = Random.Range(_soundEffect.pitchMin, _soundEffect.pitchMax);
        audioSource.volume = Random.Range(_soundEffect.volumeMin, _soundEffect.volumeMax);
        audioSource.clip = _soundEffect.audioClip;
        
        audioSource.Play();
        
        Destroy(audioSourceGameObject, _soundEffect.audioClip.length / Mathf.Abs(audioSource.pitch));
    }
    
    public void PlayerSoundEffect(SoundEffect _soundEffect, Vector3 _position) {
        GameObject audioSourceGameObject = Instantiate(audioClipSourceGameObject, _position, Quaternion.identity);
        AudioSource audioSource = audioSourceGameObject.GetComponent<AudioSource>();

        float volumeModifier = 1f / Mathf.Clamp(Vector3.Distance(_position, Camera.main.transform.position) / audioRangeMultiplier, 1f, 100000f);

        audioSource.pitch = Random.Range(_soundEffect.pitchMin, _soundEffect.pitchMax);
        audioSource.volume = Random.Range(_soundEffect.volumeMin, _soundEffect.volumeMax) * volumeModifier;
        audioSource.clip = _soundEffect.audioClip;

        audioSource.Play();

        Destroy(audioSourceGameObject, _soundEffect.audioClip.length / Mathf.Abs(audioSource.pitch));
    }

    #endregion
}
