using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SalvarLevelGame : MonoBehaviour
{
    public static SalvarLevelGame instance;

    public void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SalvarLevelsMestra(int levels, int faseMestra)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs      = File.Create(Application.persistentDataPath + "/FaseNumMestra" + faseMestra.ToString() + ".data");

        LevelClass levelObj = new LevelClass();
        levelObj.ativo = levels;
        bf.Serialize(fs, levelObj);
        fs.Close();
    }

    public int LoadLevelsMestra(int faseMestra)
    {
        int temp = 0;
        if (File.Exists(Application.persistentDataPath + "/FaseNumMestra" + faseMestra.ToString() + ".data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs      = File.Open(Application.persistentDataPath + "/FaseNumMestra" + faseMestra.ToString() + ".data", FileMode.Open);

            LevelClass levelObj = (LevelClass)bf.Deserialize(fs);
            fs.Close();
            temp = levelObj.ativo;
        }
        return temp;
    }

    public void SalvarLevel(string levelAtual, string faseMestra)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/Level" + levelAtual + "_" + faseMestra + ".data");

        LevelClass levelObj = new LevelClass();
        levelObj.ativo = 1;
        bf.Serialize(fs, levelObj);
        fs.Close();
    }

    public int LoadLevel(string levelAtual, string faseMestra)
    {
        int temp = 0;
        if (File.Exists(Application.persistentDataPath + "/Level" + levelAtual + "_" + faseMestra + ".data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/Level" + levelAtual + "_" + faseMestra + ".data", FileMode.Open);

            LevelClass levelObj = (LevelClass)bf.Deserialize(fs);
            fs.Close();
            temp = levelObj.ativo;
        }
        return temp;
    }

    [System.Serializable]
    public class LevelClass
    {
        public int ativo;
    }
}
