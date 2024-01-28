using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    private float _repeateRate = 0.5f;
    private float _prob = 0.3f;


    public void StartScreams()
    {
        InvokeRepeating("TryScream", 0, _repeateRate);
    }


    public void StopScreams()
    {
        CancelInvoke();
    }


    void TryScream()
    {
        float val = Random.Range(0f, 1f);
        int id = Random.Range(0, _audioClips.Length);

        if(val <= _prob)
        {
            _audioSource.PlayOneShot(_audioClips[id]);
        }
    }
}
