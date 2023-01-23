using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour
{
    public float amplitudeMovementY = 2; // amplitude du mouvement
    public float frequencyMovementY = 1; // fréquence du mouvement

    public float amplitudeMovementZ = 2; // amplitude du mouvement
    public float frequencyMovementZ = 1; // fréquence du mouvement

    public float amplitudeMovementX = 2; // amplitude du mouvement
    public float frequencyMovementX = 1; // fréquence du mouvement


    public float rotationAmplitudeZ = 30; // amplitude de la rotation (en degrés)
    public float rotationFrequencyZ = 1; // fréquence de la rotation

    public float rotationAmplitudeY = 30; // amplitude de la rotation (en degrés)
    public float rotationFrequencyY = 1; // fréquence de la rotation

    public float rotationAmplitudeX = 30; // amplitude de la rotation (en degrés)
    public float rotationFrequencyX = 1; // fréquence de la rotation

    private float startY; // position Y initiale de l'objet
    private float startX; // position X initiale de l'objet
    private float startZ; // position Z initiale de l'objet

    private float startRotationZ; // rotation initiale de l'objet
    private float startRotationY; // rotation initiale de l'objet
    private float startRotationX; // rotation initiale de l'objet

    void Start()
    {
        // Enregistre la position initiale de l'objet
        startY = transform.position.y;
        startX = transform.position.x;
        startZ = transform.position.z;

        // Enregistre la rotation initiale de l'objet
        startRotationZ = transform.localEulerAngles.z;

        // Enregistre la rotation initiale de l'objet
        startRotationY = transform.localEulerAngles.x;

        // Enregistre la rotation initiale de l'objet
        startRotationX = transform.localEulerAngles.x;
    }

    void Update()
    {
        // Calcule la nouvelle position de l'objet en utilisant une formule de mouvement sinusoïdal
        float newY = startY + amplitudeMovementY * Mathf.Sin(Time.time * frequencyMovementY);
        float newX = startX + amplitudeMovementX * Mathf.Sin(Time.time * frequencyMovementX);
        float newZ = startZ + amplitudeMovementZ * Mathf.Sin(Time.time * frequencyMovementZ);
        
        // Applique la nouvelle position à l'objet
        transform.position = new Vector3(newX, newY, newZ);



        // Calcule la nouvelle rotation Z de l'objet en utilisant une formule de mouvement sinusoïdal
        float newRotationZ = startRotationZ + rotationAmplitudeZ * Mathf.Sin(Time.time * rotationFrequencyZ);

        // Calcule la nouvelle rotation Y de l'objet en utilisant une formule de mouvement sinusoïdal
        float newRotationY = startRotationY + rotationAmplitudeY * Mathf.Sin(Time.time * rotationFrequencyY);

        // Calcule la nouvelle rotation X de l'objet en utilisant une formule de mouvement sinusoïdal
        float newRotationX = startRotationX + rotationAmplitudeX * Mathf.Sin(Time.time * rotationFrequencyX);


        // Applique la nouvelle rotation à l'objet
        transform.localEulerAngles = new Vector3(newRotationX, newRotationY, newRotationZ);
    }
}
