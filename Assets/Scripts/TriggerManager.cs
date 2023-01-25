using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _enterEvent;
    [SerializeField] private UnityEvent _exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _enterEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _exitEvent.Invoke();
    }
}
