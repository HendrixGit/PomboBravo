using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcoAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource aSource;
    [SerializeField]
    AudioClip[] aClips;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (!aSource.isPlaying) {
            StartCoroutine(espera());
        }    
    }

    AudioClip GetRandom() {
        return aClips[Random.Range(0, aClips.Length)];
    }

    IEnumerator espera() {
        yield return new WaitForSeconds(1);
        aSource.clip = GetRandom();
        aSource.Play();
    }

}
