using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AlarmSpace _alarmSpace;

    private Coroutine _volumeCoroutine;
    private float _minVolume = 0f;
    private float _maxVolume = 1.0f;
    private float _fadeSpeed = 0.05f;

    private void OnEnable()
    {
        _alarmSpace.Came += IncreaseVolume;
        _alarmSpace.WentOut += DecreaseVolume;
    }

    private void Start()
    {
        _audio.volume = _minVolume;

        _audio.Play();
    }

    private void OnDisable()
    {
        _alarmSpace.Came -= IncreaseVolume;
        _alarmSpace.WentOut -= DecreaseVolume;
    }

    private void IncreaseVolume()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void DecreaseVolume()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(ChangeVolume(_minVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (!Mathf.Approximately(_audio.volume, targetVolume))
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, _fadeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
