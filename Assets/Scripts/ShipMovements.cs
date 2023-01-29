using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovements : MonoBehaviour
{

    [SerializeField] float _power;
    [SerializeField] float _maxSpeed = 20.1f;
    [Space(10)]
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] private float _directionInertia = 1f;
    private float _directionStatus = 0f;
    [Space(10)]
    [SerializeField] private float _engineInertia = 1f;
    private float _engineStatus = 0f;
    [Space(10)]
    [SerializeField] private InputActionReference _thumstickActionLeft;
    [SerializeField] private InputActionReference _thumstickActionRight;

    [HideInInspector] public Rigidbody _rb;

    private AudioSource _audioSource;

    private float _waterFriction = 0.15f;

   
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
     
        Direction(thumstickValX);

        WaterFriction();
    }

    private void EngineSound(float thumstickVal)
    {
        //if (thumstickVal > 0.5f) _audioSource.volume = thumstickVal;
        //else if (thumstickVal < -0.5f) _audioSource.volume = -thumstickVal;
        //else _audioSource.volume = 0.5f;

        //if (thumstickVal > 0f) _audioSource.pitch = 1 + thumstickVal * 0.75f;
        //else _audioSource.pitch = 1 - thumstickVal * 0.75f;


        if (_engineStatus > 0.5f) _audioSource.volume = _engineStatus;
        else if (_engineStatus < -0.5f) _audioSource.volume = -_engineStatus;
        else _audioSource.volume = 0.5f;

        if (_engineStatus > 0f) _audioSource.pitch = 1 + _engineStatus * 0.75f;
        else _audioSource.pitch = 1 - _engineStatus * 0.75f;
    }

    private void WaterFriction()
    {
        _rb.velocity -= _waterFriction * _rb.velocity * Time.deltaTime;        
    }


    public void Engine(float inputValue)
    {
        _engineStatus += inputValue * _engineInertia * Time.deltaTime;
        if (_engineStatus > 1) _engineStatus = 1;
        else if (_engineStatus < -1) _engineStatus = -1;

        // Il faudra retirer le time.deltatime de Addforce, qui n'en a pas besoin.
        _rb.AddForce(transform.forward * Time.deltaTime * _engineStatus * _power);        
    }


    public void Direction(float inputValue)
    {
        
        float speedFactor = _rb.velocity.magnitude * 0.5f;
        if (speedFactor > 3f) speedFactor = 3f;
        

        // Avec ces lignes ci-dessous, la direction reste statique si le joueur n'actionne pas le stick de la direction.
        // (Je les ai �crites au d�part pour introduire une inertie dans la direction.)
        // Il serait possible de remettre la direction � z�ro progressivement. Avec peut-�tre un bool�en pour laisser choisir
        // dans Unity si on veut qu'il y ait cette remise � z�ro progressive ou non.
        _directionStatus += inputValue * _directionInertia * Time.deltaTime;
        if (_directionStatus > 1) _directionStatus = 1;
        else if (_directionStatus < -1) _directionStatus = -1;


        Vector3 rotation = new Vector3(0, _rotationSpeed * speedFactor * _directionStatus * Time.deltaTime, 0);
        transform.Rotate(rotation);


        //float speedFactor = _rb.velocity.magnitude * 0.5f;
        //if (speedFactor > 3f) speedFactor = 3f;

        //Vector3 rotation = new Vector3(0, inputValue * _rotationSpeed * speedFactor * Time.deltaTime, 0);
        //transform.Rotate(rotation);
    }
}
