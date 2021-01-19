using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisteredAudioSource : MonoBehaviour
{
    public AudioSource source;
    public float normalMultiplier = 1;
    private void Awake() {
        GameManager.onVolumeChanged += UpdateVolume;
        UpdateVolume();
    }

    private void OnDestroy() {
        GameManager.onVolumeChanged -= UpdateVolume;
    }

    public void UpdateVolume() {
        source.volume = GameManager.musicVolume * normalMultiplier;
    }
}
