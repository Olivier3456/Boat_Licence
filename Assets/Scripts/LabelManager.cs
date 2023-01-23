using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LabelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    void Start()
    {
        _text.text = gameObject.name;
    }
}
