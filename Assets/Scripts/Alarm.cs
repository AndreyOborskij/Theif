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
        ControlVolume(_maxVolume);
        
        _audio.Play();
    }

    private void DecreaseVolume()
    {
        ControlVolume(_minVolume);
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (Mathf.Approximately(_audio.volume, targetVolume) == false)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, _fadeSpeed * Time.deltaTime);

            yield return null;
        }

        if (_audio.volume == _minVolume)
        {
            Debug.Log("Выкл");
            _audio.Stop();
        }
    }

    private void ControlVolume(float targetVolume)
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(ChangeVolume(targetVolume));
    }
}
