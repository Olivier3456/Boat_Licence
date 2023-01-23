using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovements : MonoBehaviour
{

    //   [SerializeField] float _power;

    [SerializeField] float _maxSpeed = 20f;

    [SerializeField] float _rotationSpeed = 10f;

    [SerializeField] private InputActionReference _thumstickActionLeft;
    [SerializeField] private InputActionReference _thumstickActionRight;

    private Rigidbody _rb;

    private float _speed;

    private AudioSource _audioSource;

    private float _waterFriction = 0.1f;

    private float _directionInertia = 0.1f;
    private float _directionStatus = 0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;
    }


    private void Update()
    {
        // Propulsion :
        float thumstickValY = _thumstickActionLeft.action.ReadValue<Vector2>().y;
        if (_rb.velocity.magnitude < _maxSpeed) Engine(thumstickValY);
        EngineSound(thumstickValY);

        // Direction :
        float thumstickValX = _thumstickActionRight.action.ReadValue<Vector2>().x;
        _directionStatus += thumstickValX * _directionInertia * Time.deltaTime;
        if (_directionStatus > 1) _directionStatus = 1;
        else if (_directionStatus < -1) _directionStatus = -1;
        Direction(_directionStatus);

        WaterFriction();
    }

    private void EngineSound(float thumstickVal)
    {
        if (thumstickVal > 0.5f) _audioSource.volume = thumstickVal;
        else if (thumstickVal < -0.5f) _audioSource.volume = -thumstickVal;
        else _audioSource.volume = 0.5f;

        if (thumstickVal > 0f) _audioSource.pitch = 1 + thumstickVal * 0.75f;
        else _audioSource.pitch = 1 - thumstickVal * 0.75f;
    }

    private void WaterFriction()
    {
        //  _rb.velocity -= _waterFriction * _rb.velocity * Time.deltaTime;

        _speed -= _waterFriction * _rb.velocity.magnitude * Time.deltaTime;
    }


    public void Engine(float inputValue)
    {
        _speed += inputValue / 50;

        transform.Translate(transform.forward * _speed * Time.deltaTime);

        // Il faudra retirer le time.deltatime de Addforce, qui n'en a pas besoin.
        //   _rb.AddForce(transform.forward * Time.deltaTime * inputValue * _power);
    }


    public void Direction(float inputValue)
    {
        float speedFactor = _speed;
        if (speedFactor > 3f) speedFactor = 3f;
        else if (speedFactor < -3f) speedFactor = -3f;

        Vector3 rotation = new Vector3(0, inputValue * _rotationSpeed * Time.deltaTime * speedFactor, 0);
        transform.Rotate(rotation);
    }
}
