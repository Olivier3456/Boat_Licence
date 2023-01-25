using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshPro _errorsText;
    [SerializeField] private TextMeshPro _speedingText;
    [SerializeField] private TextMeshPro _finalSentenceText;
    [SerializeField] private GameManager _gameManager;

    public void DisplayErrorText()
    {
        _errorsText.text = "You have entered " + _gameManager.ForbiddenZonesErrors + " prohibited areas.";

        if (_gameManager.speeding) _speedingText.text = "You have committed a speeding ticket!";
        else _speedingText.text = "You have respected the speed limit.";

        int errorCount = 0;
        if (_gameManager.speeding) errorCount++;
        errorCount += _gameManager.ForbiddenZonesErrors;

        if (errorCount == 0)
        {
            _finalSentenceText.text = "You did it!";
        }
        else if (errorCount == 1)
        {
            _finalSentenceText.text = "You have almost succeeded!";
        }
        else
        {
            _finalSentenceText.text = "You need a little more practice.";
        }
    }
}
