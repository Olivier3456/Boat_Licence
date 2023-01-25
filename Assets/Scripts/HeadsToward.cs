using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsToward : MonoBehaviour
{
    [SerializeField] Transform objectToLookAt;
    
    
    void Update()
    {
        transform.LookAt(objectToLookAt);
    }
}
