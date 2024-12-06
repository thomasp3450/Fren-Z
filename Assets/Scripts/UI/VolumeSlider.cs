using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeSlider : MonoBehaviour {
    private float _volume;
    [SerializeField] GameObject _audioManager;
    [SerializeField] Slider _volumeSlider;
    void Start(){
        _audioManager = GameObject.Find("AudioManager");
    }
    void Update () {
        if (_volumeSlider != null) _volume = _volumeSlider.value;
        if (_audioManager != null) {
            _audioManager.GetComponent<AudioManager>().musicSource.volume = _volume;
            _audioManager.GetComponent<AudioManager>().SFXSource.volume = _volume;
        }
    }
}
