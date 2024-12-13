using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderVolumeChanger : MonoBehaviour
{
    private const float _MinDb = -80f;
    private const float _MaxDb = 0f;

    [SerializeField] private AudioMixerGroup _audioMixer;

    private float decibelScale = 20;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if(_audioMixer.audioMixer.GetFloat(_audioMixer.name, out float volume))
            _slider.value = Mathf.Log10(volume) * decibelScale;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        float dbValue = Mathf.Log10(value) * decibelScale;
        _audioMixer.audioMixer.SetFloat(_audioMixer.name, dbValue);
    }
}
