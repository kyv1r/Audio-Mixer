using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwitchSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioMixerGroup _audioMixer;

    private Button _button;
    private bool _isMute = true;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(TurnOn);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TurnOn);
    }

    private void TurnOn()
    {
        if(_isMute)
        {
            _audioSource.Play();
            _isMute = false;
        }
        else
        {
            _audioSource.Stop();
            _isMute = true;
        }
    }
}
