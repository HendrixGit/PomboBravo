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

    }

    public GameObject botao;
    public Transform  localBtn;
    public List<Level> levelList;

    void Awake()
    {
        //Destroy(GameObject.Find("UIManager(Clone)"));
        //Destroy(GameObject.Find("GameManager(Clone)"));
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

            btnNew.desbloqueado                           = level.desbloqueado;
            btnNew.GetComponent<Button>().interactable    = level.habilitado;
            btnNew.GetComponentInChildren<Text>().enabled = level.txtAtivo;
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" + btnNew.levelTxtBtn.text));

            if (SalvarEstrelas.instance.LoadEstrelas(btnNew.levelTxtBtn.text) == 1)
            {
                btnNew.estrela1.enabled = true;
            }
            else if (SalvarEstrelas.instance.LoadEstrelas(btnNew.levelTxtBtn.text) == 2) {
                btnNew.estrela1.enabled = true;
                btnNew.estrela2.enabled = true;
            }
            else if (SalvarEstrelas.instance.LoadEstrelas(btnNew.levelTxtBtn.text) == 3)
            {
                btnNew.estrela1.enabled = true;
                btnNew.estrela2.enabled = true;
                btnNew.estrela3.enabled = true;
            }
            else if (SalvarEstrelas.instance.LoadEstrelas(btnNew.levelTxtBtn.text) == 0)
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
        print("Clicado");
    }
}
