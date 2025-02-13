using System;
using UnityEngine;

public class AlarmSpace : MonoBehaviour
{
    public Action ThiefCame;
    public Action ThiefWentOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thief"))
        { 
            ThiefCame?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            ThiefWentOut?.Invoke();
        }
    }
}
