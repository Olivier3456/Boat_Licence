using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RotateWithRpm : MonoBehaviour
{

    //  private float _accelRotationSmooth = 1f;
    //   private float _accelRotationTiltAngle = 30.0f;
    float maxAngle = 22f;

    float targetOrientationX = 0f;
    float diffActuAndTgtRot = 0f;
    float newRotationX = 0f;

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
        // Pour que le bateau se cabre quand on acc�l�re (code adapt� de : https://docs.unity3d.com/ScriptReference/Transform-rotation.html).      


        _engineStatus = _ship.GetEngineStatus();
        if (_engineStatus > 0.9f) _engineStatus = 0.9f;            // Evite les l�gers soubressauts quand l'input du stick est � son maximum.
     //   else if (_engineStatus < -0.2f) _engineStatus = -0.2f;    // Le bateau se cabrera moins en marche arri�re.
     //   if (_engineStatus < 0.01f && _engineStatus > -0.01f) _engineStatus = 0.01f; // Evite que la valeur soit strictement �gale � z�ro.



        //// EN TEST. Fonctionne mais le bateau n'avance plus dans la bonne direction apr�s un changement de direction.
        //// Smoothly tilts the ship towards a target rotation.
        //float tiltAroundX;
        //if (_engineStatus >= 0) tiltAroundX = -_engineStatus * _accelRotationTiltAngle;
        //else tiltAroundX = -_engineStatus * _accelRotationTiltAngle * 0.1f;      // Le bateau se cabre moins quand on recule (car le moteur est � l'arri�re).

        //// Rotate the ship by converting the angles into a quaternion.
        //Quaternion target = Quaternion.Euler(tiltAroundX, 0, 0);

        //// Dampen towards the target rotation.
        //transform.localRotation = Quaternion.Slerp(_shipGameObject.rotation, target, _accelRotationSmooth);



        // FONCTIONNE MAIS LINEAIRE
        //float newRotationX = _shipGameObject.rotation.x + (6f * -_engineStatus);        
        //transform.localEulerAngles = new Vector3(newRotationX, _shipGameObject.rotation.y, _shipGameObject.rotation.z);


        // FONCTIONNE MAIS LINEAIRE
        //float newRotationX = _shipGameObject.rotation.x + (6f * -_engineStatus);
        //Vector3 targetOrientation = new Vector3(newRotationX, _shipGameObject.rotation.y, _shipGameObject.rotation.z);
        //Vector3 rotateToward = Vector3.Slerp(transform.localEulerAngles, targetOrientation, 1);
        //transform.localEulerAngles = new Vector3(rotateToward.x, _shipGameObject.rotation.y, _shipGameObject.rotation.z);



        //float maxAngle = 20f;
        //float targetOrientationX = _shipGameObject.rotation.x + (maxAngle * _engineStatus);
        //Vector3 targetOrientation = new Vector3(targetOrientationX, _shipGameObject.rotation.y, _shipGameObject.rotation.z);
        //float diffActuAndTgtRot = (targetOrientationX - transform.localEulerAngles.x);
        //Vector3 rotateToward = Vector3.Slerp(transform.localEulerAngles, targetOrientation, 1.01f - (diffActuAndTgtRot / maxAngle));
        //transform.localEulerAngles = new Vector3(rotateToward.x, _shipGameObject.rotation.y, _shipGameObject.rotation.z);




        targetOrientationX = _shipGameObject.rotation.x + (maxAngle * _engineStatus);
        diffActuAndTgtRot = (targetOrientationX - transform.rotation.x);
        newRotationX = transform.rotation.x + diffActuAndTgtRot / maxAngle;
        Debug.Log("targetOrientationX : " + targetOrientationX + " ; diffActuAndTgtRot : " + diffActuAndTgtRot + " ; newRotationX : " + newRotationX);
        transform.localEulerAngles = new Vector3(newRotationX, _shipGameObject.rotation.y, _shipGameObject.rotation.z);

        








        transform.position = new Vector3(transform.position.x, _startAltitude, transform.position.z);     // Le bateau reste � hauteur constante.
    }
}
