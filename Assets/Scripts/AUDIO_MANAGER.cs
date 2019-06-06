using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIO_MANAGER : MonoBehaviour
{

    public static AUDIO_MANAGER instance;
    public AudioClip[] clips;
    public AudioSource audioS;

    public int pause = -1;

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
        if (pause == 1)
        {
            audioS.Pause();
        }
        else if (!audioS.isPlaying) {
            audioS.Play();
        }
    }

    public void GetSom(int clipsA) {
        if (clipsA == 0)
        {
            audioS.clip = clips[0];
            audioS.loop = true;
            audioS.Play();
        }
        else {
            audioS.clip = clips[1];
            audioS.loop = true;
            audioS.Play();
        }
    }

    AudioClip GetRandom() {
        return clips[Random.Range(0, clips.Length)];
    }

}
