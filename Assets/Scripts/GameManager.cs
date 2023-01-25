using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshPro _errorText;
    [SerializeField] TextMeshPro _speedText;

    [SerializeField] AudioSource _errorAudioSource;
    
    public int ForbiddenZonesErrors = 0;
    [HideInInspector] public bool speeding;

    private ShipMovements _zodiac;

    private void Start()
    {
        _zodiac = GameObject.Find("Zodiac").GetComponent<ShipMovements>();
    }

    private void Update()
    {
        int speed = (int)_zodiac._rb.velocity.magnitude;
        _speedText.text = speed.ToString();
    }



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
        ForbiddenZonesErrors++;
        _errorText.text = "You are in a forbidden zone!";
        _errorAudioSource.Play();
    }


    public void ExitForbiddenZone()
    {
        _errorText.text = "";       
    }


    public void Speeding()
    {
        if (_zodiac._rb.velocity.magnitude > 5)
        {
            speeding = true;
            _errorText.text = "Speeding! The speed limit is 5 knots at 300m from the shore.";
            if (!_errorAudioSource.isPlaying) _errorAudioSource.Play();
        }        
        else EndSpeeding();
    }

    public void EndSpeeding()
    {
        _errorText.text = "";
    }
}
