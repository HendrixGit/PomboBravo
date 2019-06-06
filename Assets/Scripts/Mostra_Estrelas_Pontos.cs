using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mostra_Estrelas_Pontos : MonoBehaviour
{
    private Text estrelas, estrelas2;
    private Text pontos,   pontos2;

    private int[] estrelasVal;
    private int[] pontosVal;

    private Text Moedas;

    void Awake() {

        estrelasVal = new int[2];
        pontosVal   = new int[2];
        Moedas      = GameObject.Find("Moedas").GetComponentInChildren<Text>();
        Moedas.text = SCORE_MANAGER.instance.LoadMoedas().ToString();

        for (int a = 0; a < 2; a++) {
            for (int x = 0; x <= SalvarLevelGame.instance.LoadLevelsMestra(a + 1); x++) {
                
                estrelasVal[a] += SalvarEstrelas.instance.LoadEstrelas(x.ToString(), "Mestra"  + (a + 1).ToString());
                SalvarEstrelas.instance.SalvarEstrelasTotalMestra((a + 1).ToString(), estrelasVal[a]);

                pontosVal[a]   += SalvarPontuacao.instance.LoadPontuacao(x.ToString(), "Mestra" + (a + 1).ToString());
                SalvarPontuacao.instance.SalvarPontosTotalMestra((a + 1).ToString(), pontosVal[a]);
            }
        }

        estrelas  = GameObject.FindWithTag("textstar").GetComponent<Text>();
        estrelas2 = GameObject.FindWithTag("textstar2").GetComponent<Text>();

        estrelas.text  = SalvarEstrelas.instance.LoadEstrelasMestra1().ToString();
        estrelas2.text = SalvarEstrelas.instance.LoadEstrelasMestra2().ToString();

        pontos  = GameObject.FindWithTag("textpontos").GetComponent<Text>();
        pontos2 = GameObject.FindWithTag("textpontos2").GetComponent<Text>();

        pontos.text  = SalvarPontuacao.instance.LoadPontuacaoMestra1().ToString();
        pontos2.text = SalvarPontuacao.instance.LoadPontuacaoMestra2().ToString();
    }



}
