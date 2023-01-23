using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour
{
    public float amplitude = 2; // amplitude du mouvement
    public float frequency = 1; // fréquence du mouvement

    private float startY; // position Y initiale de l'objet

    void Start()
    {
        // Enregistre la position Y initiale de l'objet
        startY = transform.position.y;
    }

    void Update()
    {
        // Calcule la nouvelle position Y de l'objet en utilisant une formule de mouvement sinusoïdal
        float newY = startY + amplitude * Mathf.Sin(Time.time * frequency);

        // Applique la nouvelle position à l'objet
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
