using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private int LevelsMestre1 = 0, LevelsMestre2 = 2;

    void Awake()
    {
        Destroy(GameObject.Find("UI_MANAGER"));
        Destroy(GameObject.Find("GAME_MANAGER"));
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

            if (SalvarLevelGame.instance.LoadLevel(level.levelReal, OndeEstou.instance.faseMestra) == 1) {
                level.desbloqueado = 1;
                level.habilitado   = true;
                level.txtAtivo     = true;
            }

            btnNew.desbloqueado                           = level.desbloqueado;
            btnNew.GetComponent<Button>().interactable    = level.habilitado;
            btnNew.GetComponentInChildren<Text>().enabled = level.txtAtivo;
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" + level.levelReal + "_" + OndeEstou.instance.faseMestra));
            btnNew.realLevel = level.levelReal;

            if (SalvarEstrelas.instance.LoadEstrelas(btnNew.realLevel, OndeEstou.instance.faseMestra)       == 1)
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

            if (OndeEstou.instance.faseMestra == "Mestra1")
            {
                LevelsMestre1++;
                SalvarLevelGame.instance.SalvarLevelsMestra(LevelsMestre1, 1);
            }
            else
            if (OndeEstou.instance.faseMestra == "Mestra2")
            {
                LevelsMestre2++;
                SalvarLevelGame.instance.SalvarLevelsMestra(LevelsMestre2, 2);
            }

        }
    }

    public void ClickLevel(string level) {
        SceneManager.LoadScene(level);
    }
   
}
