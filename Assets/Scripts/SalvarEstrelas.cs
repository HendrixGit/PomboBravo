using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SalvarEstrelas : MonoBehaviour
{
    public static SalvarEstrelas instance;

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

    public void SalvarEstrelasLevel(string levelAtual, int estrelasNum, string faseMestra)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs      = File.Create(Application.persistentDataPath + "/Level" + levelAtual + "_" + faseMestra + "estrelas.data");

        EstrelaClass estrelasObj = new EstrelaClass();
        estrelasObj.estrelas     = estrelasNum;
        bf.Serialize(fs, estrelasObj);
        fs.Close();
    }

    public int LoadEstrelas(string levelAtual, string faseMestra)
    {
        int temp = 0;
        if (File.Exists(Application.persistentDataPath + "/Level" + levelAtual + "_" + faseMestra + "estrelas.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs      = File.Open(Application.persistentDataPath + "/Level" + levelAtual + "_" + faseMestra + "estrelas.data", FileMode.Open);

            EstrelaClass estrelasObj = (EstrelaClass)bf.Deserialize(fs);
            fs.Close();
            temp = estrelasObj.estrelas;
        }
        return temp;
    }

    [Serializable]
    class EstrelaClass
    {
        public int estrelas;
    }

}
