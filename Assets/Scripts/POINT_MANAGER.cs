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

    public void MelhorPontuacaoSave(string level, int pontos, string faseMestra) {
        if (SalvarPontuacao.instance.LoadPontuacao(level, faseMestra) == 0)
        {
            SalvarPontuacao.instance.SalvarPontuacaoLevel(level, pontos, faseMestra);
        }
        else {
            if (GAME_MANAGER.instance.pontosGame >  SalvarPontuacao.instance.LoadPontuacao(level, faseMestra)) {
                SalvarPontuacao.instance.SalvarPontuacaoLevel(level, pontos, faseMestra);
            }
        }
    }

    public int MelhorPontuacaoLoad(string level, string faseMestra) {
        if (SalvarPontuacao.instance.LoadPontuacao(level, faseMestra) != 0)
        {
            return SalvarPontuacao.instance.LoadPontuacao(level, faseMestra);
        }
        else {
            return 0;
        }
    }

}
