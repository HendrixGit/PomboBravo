using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SalvarPontuacao : MonoBehaviour
{
    public static SalvarPontuacao instance;

    void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SalvarPontuacaoLevel(string levelAtual, int pontos)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs      = File.Create(Application.persistentDataPath + "/Level" + levelAtual + "best.data");

        PontuacaoClass pontuacao = new PontuacaoClass();
        pontuacao.pontosLevel    = pontos;
        bf.Serialize(fs, pontuacao);
        fs.Close();
    }

    public int LoadPontuacao(string levelAtual)
    {
        int temp = 0;
        if (File.Exists(Application.persistentDataPath + "/Level" + levelAtual + "best.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs      = File.Open(Application.persistentDataPath + "/Level" + levelAtual + "best.data", FileMode.Open);

            PontuacaoClass pontuacao = (PontuacaoClass)bf.Deserialize(fs);
            fs.Close();
            temp = pontuacao.pontosLevel;
        }
        return temp;
    }

    [Serializable]
    class PontuacaoClass
    {
        public int pontosLevel;
    }

}
