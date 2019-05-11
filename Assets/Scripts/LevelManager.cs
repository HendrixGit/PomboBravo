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

            if (level.desbloqueado == 1) {
                btnNew.estrela1.enabled = true;
                btnNew.estrela2.enabled = true;
                btnNew.estrela3.enabled = true;
            }
            btnNovo.transform.SetParent(localBtn, false);
        }
    }

    void ClickLevel(string level) {
        SceneManager.LoadScene(level);
        print("Clicado");
    }
}
