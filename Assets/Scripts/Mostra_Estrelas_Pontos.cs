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

    void Awake() {

        estrelasVal = new int[2];
        pontosVal   = new int[2];

        for (int a = 0; a > 2; a++) {
            for (int x = 0; x <= SalvarLevelGame.instance.LoadLevelsMestra(a + 1); x++) {
                estrelasVal[a] += SalvarEstrelas.instance.LoadEstrelas(x.ToString(), (a + 1).ToString());
                SalvarEstrelas.instance.SalvarEstrelasTotalMestra((a + 1).ToString(), estrelasVal[a]);
            }
        }

        estrelas  = GameObject.FindWithTag("textstar").GetComponent<Text>();
        estrelas2 = GameObject.FindWithTag("textstar").GetComponent<Text>();

        pontos  = GameObject.FindWithTag("textpontos").GetComponent<Text>();
        pontos2 = GameObject.FindWithTag("textpontos2").GetComponent<Text>();



    }

}
