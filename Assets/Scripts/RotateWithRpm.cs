using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithRpm : MonoBehaviour
{

    private float _accelRotationSmooth = 10.0f;
    private float _accelRotationTiltAngle = 10.0f;

    private float _startAltitude;
    private float _engineStatus;
    private ShipMovements _ship;

    [SerializeField] private Transform _shipGameObject;

    void Start()
    {
        _startAltitude = transform.position.y;
        _ship = GameObject.Find("Zodiac").GetComponent<ShipMovements>();
    }

    
    void Update()
    {
        // Pour que le bateau se cabre quand on accélère (code adapté de : https://docs.unity3d.com/ScriptReference/Transform-rotation.html).      


        //// Smoothly tilts the ship towards a target rotation.
        //float tiltAroundX;
        //if (engineStatus >= 0) tiltAroundX = -engineStatus * _accelRotationTiltAngle;
        //else tiltAroundX = -engineStatus * _accelRotationTiltAngle * 0.1f;      // Le bateau se cabre moins quand on recule (car le moteur est à l'arrière).

        //// Rotate the ship by converting the angles into a quaternion.
        //Quaternion target = Quaternion.Euler(tiltAroundX, 0, 0);

        //// Dampen towards the target rotation.
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _accelRotationSmooth);


        _engineStatus = _ship.GetEngineStatus();
        if (_engineStatus > 0.9f) _engineStatus = 0.9f;            // Evite les légers soubressauts quand l'input du stick est à son maximum.
        else if (_engineStatus < -0.2f) _engineStatus = - 0.2f;    // Le bateau se cabrera moins en marche arrière.

        float newRotationX = _shipGameObject.rotation.x + (6f * -_engineStatus);
        
        transform.localEulerAngles = new Vector3(newRotationX, _shipGameObject.rotation.y, _shipGameObject.rotation.z);


        transform.position = new Vector3(transform.position.x, _startAltitude, transform.position.z);     // Le bateau reste à hauteur constante.
    }
}
