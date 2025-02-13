using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AlarmSpace _alarmSpace;

    private Coroutine _volumeCoroutine;
    private float _minVolume = 0f;
    private float _maxVolume = 1.0f;
    private float _fadeSpeed = 0.1f;

    private void OnEnable()
    {
        _alarmSpace.ThiefCame += IncreaseVolume;
        _alarmSpace.ThiefWentOut += DecreaseVolume;
    }

    private void Start()
    {
        _audio.volume = _minVolume;
    }

    private void OnDisable()
    {
        _alarmSpace.ThiefCame -= IncreaseVolume;
        _alarmSpace.ThiefWentOut -= DecreaseVolume;
    }

    private void IncreaseVolume()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _audio.Play();
        _volumeCoroutine = StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void DecreaseVolume()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(ChangeVolume(_minVolume, _audio));
    }

    private IEnumerator ChangeVolume(float targetVolume, AudioSource audio = null)
    {
        while (!Mathf.Approximately(_audio.volume, targetVolume))
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, _fadeSpeed * Time.deltaTime);

            yield return null;
        }

        audio.Stop();
    }
}
