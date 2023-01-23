using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        _event.Invoke();
    }
}
