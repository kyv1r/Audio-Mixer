using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private AudioStrings _audioStrings;
    [SerializeField] private MuterSound _muterSound;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if(_audioMixer.audioMixer.GetFloat(_audioStrings.ToString(), out float volume))
            _slider.value = Mathf.Log10(volume) * 20;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        _audioMixer.audioMixer.SetFloat(_audioStrings.ToString(), Mathf.Log10(volume) * 20);

        if(volume > 0.01f)
        {
            _muterSound.SetToggleState(true);
        }
    }
}
