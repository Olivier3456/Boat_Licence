using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadThumstick : MonoBehaviour
{

  //  [SerializeField] float _power;

    [SerializeField] private InputActionReference _thumstickAction;


    void Update()
    {
        float thumstickVal = _thumstickAction.action.ReadValue<Vector2>().y;


        Debug.Log("Thumstick value = " + thumstickVal);
    }
}
