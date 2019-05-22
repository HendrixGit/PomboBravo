using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIO_MANAGER : MonoBehaviour
{

    public static AUDIO_MANAGER instance;
    public AudioClip[] clips;
    public AudioSource audioS;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioS.isPlaying) {
            audioS.clip = GetRandom();
            audioS.Play();
        }
    }

    AudioClip GetRandom() {
        return clips[Random.Range(0, clips.Length)];
    }

}
