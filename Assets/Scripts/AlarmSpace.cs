using System;
using UnityEngine;

public class AlarmSpace : MonoBehaviour
{
    public Action Came;
    public Action WentOut;

    private void OnTriggerEnter(Collider other)
    {
        Came?.Invoke();
    }
    
    private void OnTriggerExit(Collider other)
    {
        WentOut?.Invoke();
    }
}
