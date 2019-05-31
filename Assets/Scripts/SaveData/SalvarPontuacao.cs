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

    public void SalvarPontosTotalMestra(string faseMestra, int pontos)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/Mestra" + faseMestra + "pontos.data");

        PontuacaoClass pontuacao = new PontuacaoClass();
        pontuacao.pontosLevel    = pontos;
        bf.Serialize(fs, pontuacao);
        fs.Close();
    }

    public int LoadPontuacaoMestra1()
    {
        int temp = 0;
        if (File.Exists(Application.persistentDataPath + "/Mestra1pontos.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs      = File.Open(Application.persistentDataPath + "/Mestra1pontos.data", FileMode.Open);

            PontuacaoClass pontuacao = (PontuacaoClass)bf.Deserialize(fs);
            fs.Close();
            temp = pontuacao.pontosLevel;
        }
        return temp;
    }

    public int LoadPontuacaoMestra2()
    {
        int temp = 0;
        if (File.Exists(Application.persistentDataPath + "/Mestra2pontos.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs      = File.Open(Application.persistentDataPath + "/Mestra2pontos.data", FileMode.Open);

            PontuacaoClass pontuacao = (PontuacaoClass)bf.Deserialize(fs);
            fs.Close();
            temp = pontuacao.pontosLevel;
        }
        return temp;
    }

    public void SalvarPontuacaoLevel(string levelAtual, int pontos, string faseMestra)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs      = File.Create(Application.persistentDataPath + "/Level" + levelAtual + "best" + faseMestra + ".data");

        PontuacaoClass pontuacao = new PontuacaoClass();
        pontuacao.pontosLevel    = pontos;
        bf.Serialize(fs, pontuacao);
        fs.Close();
    }

    public int LoadPontuacao(string levelAtual, string faseMestra)
    {
        int temp = 0;
        if (File.Exists(Application.persistentDataPath + "/Level" + levelAtual + "best" + faseMestra + ".data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs      = File.Open(Application.persistentDataPath + "/Level" + levelAtual + "best" + faseMestra + ".data", FileMode.Open);

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
