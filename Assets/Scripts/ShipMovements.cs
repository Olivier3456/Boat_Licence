using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShipMovements : MonoBehaviour
{

    [SerializeField] float _power;
    [SerializeField] float _maxSpeed = 20.1f;
    [SerializeField] Slider _rpmSlider;
    [Space(10)]
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] private float _directionInertia = 1f;
    [SerializeField] private Slider _directionSlider;
    [SerializeField] private bool _directionFollowsStickValue;
    private float _directionStatus = 0f;
    [Space(10)]
    [SerializeField] private float _engineInertia = 1f;
    [SerializeField] private bool _engineRpmFollowsStickValue;
    private float _engineStatus = 0f;
    [Space(10)]
    [SerializeField] private InputActionReference _thumstickActionLeft;
    [SerializeField] private InputActionReference _thumstickActionRight;

    [HideInInspector] public Rigidbody _rb;

    private AudioSource _audioSource;

    private float _waterFriction = 0.15f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {       
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;

        _directionSlider.maxValue = 1;
        _directionSlider.minValue = -1;
        _rpmSlider.maxValue= 1;
        _rpmSlider.minValue= -1;
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
        if (!_engineRpmFollowsStickValue)       // Le régime moteur reste statique si le joueur n'actionne pas le stick.
        {
            _engineStatus += inputValue * _engineInertia * Time.deltaTime;
            if (_engineStatus > 1) _engineStatus = 1;
            else if (_engineStatus < -1) _engineStatus = -1;
        }
        else if (_engineStatus > inputValue + 0.01f)    // Le régime moteur se cale en permanence sur l'input value du stick.
            _engineStatus -= _engineInertia * Time.deltaTime;

        else if (_engineStatus < inputValue - 0.01f)    // Le régime moteur se cale en permanence sur l'input value du stick.
            _engineStatus += _engineInertia * Time.deltaTime;


        _rpmSlider.value = _engineStatus;

        // Il faudra retirer le time.deltatime de Addforce, qui n'en a pas besoin. Et régler la variable _power en conséquence.
        _rb.AddForce(transform.forward * Time.deltaTime * _engineStatus * _power);
    }


    public void Direction(float inputValue)
    {

        float speedFactor = _rb.velocity.magnitude * 0.5f;  // Le bateau tournera moins vite si sa vitesse est proche de zéro.
        if (speedFactor > 3f) speedFactor = 3f;


        if (!_directionFollowsStickValue)       // La direction reste statique si le joueur n'actionne pas le stick.
        {
            _directionStatus += inputValue * _directionInertia * Time.deltaTime;
            if (_directionStatus > 1) _directionStatus = 1;
            else if (_directionStatus < -1) _directionStatus = -1;
        }
        else if (_directionStatus > inputValue + 0.01f)    // La direction se cale en permanence sur l'input value du stick.
            _directionStatus -= _directionInertia * Time.deltaTime;

        else if (_directionStatus < inputValue - 0.01f)    // La direction se cale en permanence sur l'input value du stick.
            _directionStatus += _directionInertia * Time.deltaTime;


        // Avec ces lignes ci-dessous, la direction reste statique si le joueur n'actionne pas le stick de la direction.
        // (Je les ai écrites au départ pour introduire une inertie dans la direction.)
        //_directionStatus += inputValue * _directionInertia * Time.deltaTime;
        //if (_directionStatus > 1) _directionStatus = 1;
        //else if (_directionStatus < -1) _directionStatus = -1;


        Vector3 rotation = new Vector3(0, _rotationSpeed * speedFactor * _directionStatus * Time.deltaTime, 0);
        transform.Rotate(rotation);

        _directionSlider.value = _directionStatus;        
    }
}
