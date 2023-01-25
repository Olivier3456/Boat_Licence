using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshPro _errorText;
    private int errors = 0;

    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void EnterForbiddenZone()
    {
        errors++;
        _errorText.text = "You are in a forbidden zone!";
    }


    public void ExitForbiddenZone()
    {
        _errorText.text = "";
    }
}
