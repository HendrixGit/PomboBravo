using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POINT_MANAGER : MonoBehaviour
{
    public static POINT_MANAGER instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void MelhorPontuacaoSave(string level, int pontos) {
        if (SalvarPontuacao.instance.LoadPontuacao(level) == 0)
        {
            SalvarPontuacao.instance.SalvarPontuacaoLevel(level, pontos);
        }
        else {
            if (GAME_MANAGER.instance.pontosGame >  SalvarPontuacao.instance.LoadPontuacao(level)) {
                SalvarPontuacao.instance.SalvarPontuacaoLevel(level, pontos);
            }
        }
    }

    public int MelhorPontuacaoLoad(string level) {
        if (SalvarPontuacao.instance.LoadPontuacao(level) != 0)
        {
            return SalvarPontuacao.instance.LoadPontuacao(level);
        }
        else {
            return 0;
        }
    }

}
