using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelManager : MonoBehaviour
{

    [System.Serializable]
    public class Level {
        public string levelText;
        public bool   habilitado;
        public int    desbloqueado;
        public bool   txtAtivo;
        public string levelReal;

    }

    public GameObject botao;
    public Transform  localBtn;
    public List<Level> levelList;

    void Awake()
    {
        Destroy(GameObject.Find("UIManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }


    void Start()
    {
        ListaAdd();
    }


    void Update()
    {

    }

    void ListaAdd() {
        foreach (Level level in levelList) {
            GameObject btnNovo      = Instantiate(botao) as GameObject;
            BotaoLevel btnNew       = btnNovo.GetComponent<BotaoLevel>();
            btnNew.levelTxtBtn.text = level.levelText;

            if (LoadLevel(btnNew.realLevel, OndeEstou.instance.faseMestra) == 1) {
                level.desbloqueado = 1;
                level.habilitado   = true;
                level.txtAtivo     = true;
            }

            btnNew.desbloqueado                           = level.desbloqueado;
            btnNew.GetComponent<Button>().interactable    = level.habilitado;
            btnNew.GetComponentInChildren<Text>().enabled = level.txtAtivo;
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" + level.levelReal + "_" + OndeEstou.instance.faseMestra));
            btnNew.realLevel = level.levelReal;

            if (SalvarEstrelas.instance.LoadEstrelas(btnNew.realLevel, OndeEstou.instance.faseMestra) == 1)
            {
                btnNew.estrela1.enabled = true;
            }
            else if (SalvarEstrelas.instance.LoadEstrelas(btnNew.realLevel, OndeEstou.instance.faseMestra) == 2) {
                btnNew.estrela1.enabled = true;
                btnNew.estrela2.enabled = true;
            }
            else if (SalvarEstrelas.instance.LoadEstrelas(btnNew.realLevel, OndeEstou.instance.faseMestra) == 3)
            {
                btnNew.estrela1.enabled = true;
                btnNew.estrela2.enabled = true;
                btnNew.estrela3.enabled = true;
            }
            else if (SalvarEstrelas.instance.LoadEstrelas(btnNew.realLevel, OndeEstou.instance.faseMestra) == 0)
            {
                btnNew.estrela1.enabled = false;
                btnNew.estrela2.enabled = false;
                btnNew.estrela3.enabled = false;
            }

            btnNovo.transform.SetParent(localBtn, false);
        }
    }

    public void ClickLevel(string level) {
        SceneManager.LoadScene(level);
    }

    public void SalvarLevel(string levelAtual, string faseMestra) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs      = File.Create(Application.persistentDataPath + "/Level" + levelAtual + "_" + faseMestra + ".data");

        LevelClass levelObj = new LevelClass();
        levelObj.ativo      = 1;
        bf.Serialize(fs, levelObj);
        fs.Close();
    }

    public int LoadLevel(string levelAtual, string faseMestra)
    {
        int temp = 0;
        if (File.Exists(Application.persistentDataPath + "/Level" + levelAtual + "_" + faseMestra +  ".data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs      = File.Open(Application.persistentDataPath + "/Level" + levelAtual + "_" + faseMestra + ".data", FileMode.Open);

            LevelClass levelObj = (LevelClass)bf.Deserialize(fs);
            fs.Close();
            temp = levelObj.ativo;
        }
        return temp;
    }

    [System.Serializable]
    public class LevelClass {
        public int ativo;
    }

}
