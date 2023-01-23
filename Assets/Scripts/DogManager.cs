using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;

public class DogManager : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _destination;
    private AudioSource _audioSource;
    private Animator _animator;

    [SerializeField] private AudioClip[] _audioClips;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClips[0];
        _agent.SetDestination(_destination.position);
    }


    void Update()
    {
        if (_agent.remainingDistance < 0.5f)
        {
            _agent.isStopped = true;
            _animator.SetBool("IsRunning", false);            
            if (!_audioSource.isPlaying) _audioSource.Play();
        }
        else
        {
            _agent.isStopped = false;
            _animator.SetBool("IsRunning", true);
        }

        _agent.SetDestination(_destination.position);
    }

    public void ToggleIsPet()
    {
       _animator.SetBool("IsPet", !_animator.GetBool("IsPet"));       
    }

    //public void IsPet()
    //{
    //    _animator.SetBool("IsPet", true);
    //    _audioSource.clip = _audioClips[1];
    //    _audioSource.Play();
    //    StartCoroutine(EndPet());
    //}

    //IEnumerator EndPet()
    //{
    //    yield return new WaitForSeconds(2);
    //    _animator.SetBool("IsPet", false);
    //    _audioSource.clip = _audioClips[0];
    //}

}
