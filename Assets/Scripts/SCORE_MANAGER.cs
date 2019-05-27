using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SCORE_MANAGER : MonoBehaviour
{
    public static SCORE_MANAGER instance;

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

    public void PerdeMoedas(int moeda)
    {
        int temp      = LoadMoedas();
        int novoValor = temp - moeda;

        SalvarDadosMoedas(novoValor);
    }

    public void SalvarDadosMoedas(int moeda) {
        BinaryFormatter bf   = new BinaryFormatter();
        FileStream fs        = File.Create(Application.persistentDataPath + "/coinData.data");
        SaveMoedasClass coin = new SaveMoedasClass();
        coin.moedas = moeda;
        bf.Serialize(fs, coin);
        fs.Close();

    }

    public int LoadMoedas() {
        int moedas = 0;
        if (File.Exists(Application.persistentDataPath + "/coinData.data")) {
            BinaryFormatter bf   = new BinaryFormatter();
            FileStream fs        = File.Open(Application.persistentDataPath + "/coinData.data", FileMode.Open);
            SaveMoedasClass coin = (SaveMoedasClass)bf.Deserialize(fs);
            fs.Close();
            moedas = coin.moedas;
        }
        return moedas;
    }

    [Serializable]
    class SaveMoedasClass {
        public int moedas;
    }

}
