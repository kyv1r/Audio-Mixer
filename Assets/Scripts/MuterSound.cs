using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class MuterSound : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private List<Slider> _soundSlider;

    private float _minVolume = -80;
    private float _savedVolume = 0;
    private Toggle _toggle;

    public bool ToggleState => _toggle.isOn;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnMuteToggleChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnMuteToggleChanged);
        SyncToggleWithVolume();
    }

    private void OnMuteToggleChanged(bool isMuted)
    {
        if (isMuted)
        {
            foreach (var slider in _soundSlider)
                slider.enabled = false;

            if (_audioMixer.audioMixer.GetFloat(_audioMixer.name, out float currentVolume))
                _savedVolume = currentVolume;

            _audioMixer.audioMixer.SetFloat(_audioMixer.name, _minVolume);
        }
        else
        {
            foreach(var slider in _soundSlider)
                slider.enabled = true;

            _audioMixer.audioMixer.SetFloat(_audioMixer.name, _savedVolume);
        }
    }

    private void SyncToggleWithVolume()
    {
        if (_audioMixer.audioMixer.GetFloat(_audioMixer.name, out float currentVolume))
        {
            _toggle.isOn = currentVolume > _minVolume;
        }
    }

}
