using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAME_MANAGER : MonoBehaviour
{
    public static GAME_MANAGER instance;
    public GameObject[] passaro;
    public int passarosNum;
    public int passarosEmCena = 0;
    public Transform pos;
    public bool win = false, jogoComecou;
    public string nomePassaro;

    public bool passaroLancado = false;
    public Transform objE, objD;
    public int numPorcosCena;
    private bool tocaWin, tocaLose = false;

    public bool estrela1Fim, estrela2Fim;
    public int aux;

    public int estrelasNum;
    public bool trava = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Carrega;

    }

    void Carrega(Scene cena, LoadSceneMode modo) {
        pos = GameObject.FindWithTag("pos").GetComponent<Transform>();

        //passaroPos
        passarosNum = GameObject.FindGameObjectsWithTag("Player").Length;
        passaro = new GameObject[passarosNum];
        objE = GameObject.FindGameObjectWithTag("PE").GetComponent<Transform>();
        objD = GameObject.FindGameObjectWithTag("PD").GetComponent<Transform>();
        StartGame();

        for (int x = 0; x < passarosNum; x++) {
            passaro[x] = GameObject.Find("Bird" + x);
        }

        numPorcosCena = GameObject.FindGameObjectsWithTag("porco").Length;
        aux = passarosNum;

    }

    void NascPassaro() {
        if (passarosEmCena == 0 && passarosNum > 0) {
            for (int x = 0; x < passaro.Length; x++) {
                if (passaro[x] != null) {
                    if (passaro[x].transform.position != pos.position && passarosEmCena == 0) {
                        nomePassaro = passaro[x].name;
                        passaro[x].transform.position = pos.position;
                        passarosEmCena = 1;
                    }
                }
            }
        }
    }

    void GameOver()
    {
        jogoComecou = false;
    }

    void WinGame() {
        if (jogoComecou != false) {
            jogoComecou = false;
            UI_MANAGER.instance.painelWin.Play("MenuWin_Anim");

            if (!UI_MANAGER.instance.winSom.isPlaying && tocaWin == false)
            {
                UI_MANAGER.instance.winSom.Play();
                tocaWin = true;
            }

            //pontos


        }
                
        if (tocaWin && !UI_MANAGER.instance.winSom.isPlaying && trava == false) {
            if (passarosNum == aux - 1)
            {
                UI_MANAGER.instance.estrela1.Play("Estrela1_Anim");
                if (estrela1Fim)
                {
                    UI_MANAGER.instance.estrela2.Play("Estrela2_Anim");
                    if (estrela2Fim)
                    {
                        UI_MANAGER.instance.estrela3.Play("Estrela3_Anim");
                        trava = true;
                    }
                }

                estrelasNum = 3;

            }
            else if (passarosNum == aux - 2)
            {
                UI_MANAGER.instance.estrela1.Play("Estrela1_Anim");
                if (estrela1Fim)
                {
                    UI_MANAGER.instance.estrela2.Play("Estrela2_Anim");
                    trava = true;
                }

                estrelasNum = 2;

            }

            else if (passarosNum <= aux - 3)
            {
                UI_MANAGER.instance.estrela1.Play("Estrela1_Anim");
                trava = true;
                estrelasNum = 1;
            }
            else {
                estrelasNum = 0;
                trava = true;
            }

            if (SalvarEstrelas.instance.LoadEstrelas(OndeEstou.instance.fase.ToString()) == 0)
            {
                SalvarEstrelas.instance.SalvarEstrelasLevel(OndeEstou.instance.fase.ToString(), estrelasNum);
            }
            else {
                if (SalvarEstrelas.instance.LoadEstrelas(OndeEstou.instance.fase.ToString()) < estrelasNum) {
                    SalvarEstrelas.instance.SalvarEstrelasLevel(OndeEstou.instance.fase.ToString(), estrelasNum);
                }
            }

        }
    }

    void StartGame() {
        jogoComecou = true;
        passarosEmCena = 0;
        win = false;
    }

    void Start()
    {
        if (instance == null) {

        }
    }

    void Update()
    {
        if (numPorcosCena <= 0 && passarosNum > 0) {
            win = true;
        }

        if (win == true)
        {
            WinGame();
        }
        else {
            NascPassaro();
        }
    }
}
