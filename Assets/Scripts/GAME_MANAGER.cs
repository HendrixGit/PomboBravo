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
    public bool lose = false;

    public bool estrela1Fim, estrela2Fim;
    public int aux;

    public int estrelasNum;
    public bool trava = false;

    public int pontosGame, bestPontoGame;
    public int moedasGame;
    public bool pausado = false;

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

        if (OndeEstou.instance.fase >= 1 && OndeEstou.instance.fase <= 4)
        {

            pos = GameObject.FindWithTag("pos").GetComponent<Transform>();

            //passaroPos
            passarosNum = GameObject.FindGameObjectsWithTag("Player").Length;
            passaro = new GameObject[passarosNum];
            objE = GameObject.FindGameObjectWithTag("PE").GetComponent<Transform>();
            objD = GameObject.FindGameObjectWithTag("PD").GetComponent<Transform>();
            StartGame();

            for (int x = 0; x < passarosNum; x++)
            {
                passaro[x] = GameObject.Find("Bird" + x);
            }

            numPorcosCena = GameObject.FindGameObjectsWithTag("porco").Length;
            aux = passarosNum;
        }

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
        UI_MANAGER.instance.painelGameOver.Play("MenuLose_Anim");
        if (!UI_MANAGER.instance.loseSom.isPlaying && tocaLose == false) {
            UI_MANAGER.instance.loseSom.Play();
            tocaLose = true;
        }
    }

    void WinGame() {

        SCORE_MANAGER.instance.SalvarDadosMoedas(moedasGame);

        if (jogoComecou != false) {
            jogoComecou = false;
            UI_MANAGER.instance.painelWin.Play("MenuWin_Anim");

            if (!UI_MANAGER.instance.winSom.isPlaying && tocaWin == false)
            {
                UI_MANAGER.instance.winSom.Play();
                tocaWin = true;
            }

            //pontos
            POINT_MANAGER.instance.MelhorPontuacaoSave(OndeEstou.instance.fase.ToString(), pontosGame, OndeEstou.instance.faseMestra);
            int tempOnde = OndeEstou.instance.fase + 1;
            SalvarLevelGame.instance.SalvarLevel(tempOnde.ToString(), OndeEstou.instance.faseMestra);

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

                        UI_MANAGER.instance.winBtnMenu.interactable      = true;
                        UI_MANAGER.instance.winBtnNovamente.interactable = true;
                        UI_MANAGER.instance.WinBtnProximo.interactable   = true;
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

                    UI_MANAGER.instance.winBtnMenu.interactable = true;
                    UI_MANAGER.instance.winBtnNovamente.interactable = true;
                    UI_MANAGER.instance.WinBtnProximo.interactable = true;
                }

                estrelasNum = 2;

            }

            else if (passarosNum <= aux - 3)
            {
                UI_MANAGER.instance.estrela1.Play("Estrela1_Anim");
                trava = true;
                estrelasNum = 1;

                UI_MANAGER.instance.winBtnMenu.interactable = true;
                UI_MANAGER.instance.winBtnNovamente.interactable = true;
                UI_MANAGER.instance.WinBtnProximo.interactable = true;
            }
            else {
                estrelasNum = 0;
                trava = true;
            }

            if (SalvarEstrelas.instance.LoadEstrelas(OndeEstou.instance.fase.ToString(), OndeEstou.instance.faseMestra) == 0)
            {
                SalvarEstrelas.instance.SalvarEstrelasLevel(OndeEstou.instance.fase.ToString(), estrelasNum, OndeEstou.instance.faseMestra);
            }
            else {
                if (SalvarEstrelas.instance.LoadEstrelas(OndeEstou.instance.fase.ToString(), OndeEstou.instance.faseMestra) < estrelasNum) {
                    SalvarEstrelas.instance.SalvarEstrelasLevel(OndeEstou.instance.fase.ToString(), estrelasNum, OndeEstou.instance.faseMestra);
                }
            }

        }
    }

    void StartGame() {
        jogoComecou    = true;
        passarosEmCena = 0;
        win         = false;
        lose        = false;
        tocaWin     = false;
        tocaLose    = false;
        estrelasNum = 0;
        trava       = false;

        pontosGame     = 0;
        bestPontoGame  = POINT_MANAGER.instance.MelhorPontuacaoLoad(OndeEstou.instance.fase.ToString(), OndeEstou.instance.faseMestra);
        passaroLancado = false;//resolve bug passaro ao pausar de a camera nao acompanhar na morte do mesmo

        UI_MANAGER.instance.pontosTxt.text    = pontosGame.ToString();
        UI_MANAGER.instance.bestPontoTxt.text = bestPontoGame.ToString();
        UI_MANAGER.instance.moedasTxt.text    = SCORE_MANAGER.instance.LoadMoedas().ToString();
        moedasGame                            = SCORE_MANAGER.instance.LoadMoedas();

        UI_MANAGER.instance.winBtnMenu.interactable      = false;
        UI_MANAGER.instance.winBtnNovamente.interactable = false;
        UI_MANAGER.instance.WinBtnProximo.interactable   = false;
    }

    void Start() {
        StartGame();
    }


    void Update()
    {        
        if (numPorcosCena <= 0 && passarosNum > 0)
        {
            win = true;
        }
        else if (numPorcosCena > 0 && passarosNum <= 0)
        {
            lose = true;
        }

        if (win == true)
        {
            WinGame();
        }
        else if (lose)
        {
            GameOver();
        }

        if (jogoComecou)
        {
            NascPassaro();
        }
    }
}
