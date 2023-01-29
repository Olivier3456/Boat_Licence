using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour
{
    [Header("Translations")]
    public float amplitudeMovementX = 2;
    public float frequencyMovementX = 1;
    [Space(10)]
    public float amplitudeMovementY = 2;
    public float frequencyMovementY = 1;
    [Space(10)]
    public float amplitudeMovementZ = 2;
    public float frequencyMovementZ = 1;
    [Space(20)]
    [Header("Rotations")]
    public float rotationAmplitudeX = 30;
    public float rotationFrequencyX = 1;
    [Space(10)]
    public float rotationAmplitudeY = 30;
    public float rotationFrequencyY = 1;
    [Space(10)]
    public float rotationAmplitudeZ = 30;
    public float rotationFrequencyZ = 1;

    [SerializeField] private Transform _shipGameObject;

       
    void Update()
    {
        // Calcule la nouvelle position de l'objet en utilisant une formule de mouvement sinusoïdal
        float newX = _shipGameObject.position.x + amplitudeMovementX * Mathf.Sin(Time.time * frequencyMovementX) * 0.05f;
        float newY = _shipGameObject.position.y + amplitudeMovementY * Mathf.Sin(Time.time * frequencyMovementY) * 0.05f;
        float newZ = _shipGameObject.position.z + amplitudeMovementZ * Mathf.Sin(Time.time * frequencyMovementZ) * 0.05f;

        transform.position = new Vector3(newX, newY, newZ);


        // Calcule la nouvelle rotation de l'objet en utilisant une formule de mouvement sinusoïdal
        float newRotationZ = _shipGameObject.rotation.z + rotationAmplitudeZ * Mathf.Sin(Time.time * rotationFrequencyZ);
        float newRotationY = _shipGameObject.rotation.y + rotationAmplitudeY * Mathf.Sin(Time.time * rotationFrequencyY);
        float newRotationX = _shipGameObject.rotation.x + rotationAmplitudeX * Mathf.Sin(Time.time * rotationFrequencyX);

        transform.localEulerAngles = new Vector3(newRotationX, newRotationY, newRotationZ);
    }
}
