using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _enterEvent;
    [SerializeField] private UnityEvent _exitEvent;
    [SerializeField] private UnityEvent _stayEvent;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stayEvent.Invoke();
        }
    }
}
