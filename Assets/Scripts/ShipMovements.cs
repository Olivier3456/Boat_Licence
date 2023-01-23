using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovements : MonoBehaviour
{

    [SerializeField] float _power;

    [SerializeField] float _maxSpeed = 100f;

    [SerializeField] private InputActionReference _thumstickAction;

    private Rigidbody _rb;

    private float _waterFriction = 0.1f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        float thumstickVal = _thumstickAction.action.ReadValue<Vector2>().y;
     //   Debug.Log("Thumstick value = " + thumstickVal);

        if (_rb.velocity.magnitude < _maxSpeed) Motor(thumstickVal);

        WaterFriction();
    }


    private void WaterFriction()
    {
        _rb.velocity -= _waterFriction * _rb.velocity * Time.deltaTime;
    }



    public void Motor(float inputValue)
    {
        _rb.AddRelativeForce(Vector3.forward * Time.deltaTime * inputValue * _power);
        Debug.Log("Le moteur tourne.");
    }
}
