using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class MuterSound : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private AudioStrings _audioStrings;

    private float _minVolume = -80;
    private float _maxVolume = 0;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(Mute);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveAllListeners();
    }

    private void Mute(bool enabled)
    {
        if (enabled)
            _audioMixer.audioMixer.SetFloat(_audioStrings.ToString(), _maxVolume);
        else
            _audioMixer.audioMixer.SetFloat(_audioStrings.ToString(), _minVolume);
    }

    public void SetToggleState(bool state)
    {
        _toggle.isOn = state;
    }
}
