using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StudioLightManager : MonoBehaviour
{
    private Transform[] _children;

    private void Start()
    {
       _children = transform.GetComponentsInChildren<Transform>();
    }


    public void ToggleLights()
    {
        foreach (var child in _children)
        {
            GameObject gm = child.gameObject;
            if (gm.activeSelf) gm.SetActive(false);
            else gm.SetActive(true);
        }
    }
}
