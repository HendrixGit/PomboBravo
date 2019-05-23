using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrelas_Audio : MonoBehaviour
{

    public AudioSource aSouce;
    public AudioClip   clip;
    public string      nomeEstrela;

    public void TocaAudioEstrela() {
        if (!aSouce.isPlaying) {
            aSouce.clip = clip;
            aSouce.Play();
        }
    }

    public void Verifica_Star() {
        switch (nomeEstrela) {
            case "Estrela1_Win": GAME_MANAGER.instance.estrela1Fim = true; break;
            case "Estrela2_Win": GAME_MANAGER.instance.estrela2Fim = true; break;
        }
    }

}
