using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MANAGER : MonoBehaviour
{
    public static UI_MANAGER instance;
    public Animator painelGameOver, painelWin, painelPause;
    public Button winBtnMenu, winBtnNovamente, WinBtnProximo;
    public Animator estrela1, estrela2, estrela3;
    [SerializeField]
    private Button loseBtnMenu, loseBtnNovamente;
    [SerializeField]
    private Button pauseBtn, pauseBtnPlay, pauseBtnNovamente, pauseBtnMenu, pauseBtnLoja;
    public AudioSource winSom, loseSom;
    public Text pontosTxt, bestPontoTxt;
    public Text moedasTxt;
    private Image fundoPreto;

    void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo) {

        if (OndeEstou.instance.fase >= 1 && OndeEstou.instance.fase <= 4)
        {

            print("OKOK");
            //Painel
            painelGameOver = GameObject.Find("MenuLose").GetComponent<Animator>();
            painelWin = GameObject.Find("MenuWin").GetComponent<Animator>();
            painelPause = GameObject.Find("Painel_Pause").GetComponent<Animator>();

            //btnwin
            winBtnMenu = GameObject.Find("Button_Menu").GetComponent<Button>();
            winBtnNovamente = GameObject.Find("Button_Novamente").GetComponent<Button>();
            WinBtnProximo = GameObject.Find("Button_Avancar").GetComponent<Button>();

            //Estrelas
            estrela1 = GameObject.Find("Estrela1_Win").GetComponent<Animator>();
            estrela2 = GameObject.Find("Estrela2_Win").GetComponent<Animator>();
            estrela3 = GameObject.Find("Estrela3_Win").GetComponent<Animator>();

            //btnLose
            loseBtnMenu = GameObject.Find("Button_Menu_L").GetComponent<Button>();
            loseBtnNovamente = GameObject.Find("Button_Novamente_L").GetComponent<Button>();

            //btnPause
            pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
            pauseBtnPlay = GameObject.Find("play").GetComponent<Button>();
            pauseBtnNovamente = GameObject.Find("again").GetComponent<Button>();
            pauseBtnMenu = GameObject.Find("scene").GetComponent<Button>();
            pauseBtnLoja = GameObject.Find("shop").GetComponent<Button>();

            //audio
            winSom = painelWin.GetComponent<AudioSource>();
            loseSom = painelGameOver.GetComponent<AudioSource>();

            //Pontos
            pontosTxt = GameObject.FindGameObjectWithTag("pointval").GetComponent<Text>();
            bestPontoTxt = GameObject.FindGameObjectWithTag("ptbest").GetComponent<Text>();

            //moedas
            moedasTxt = GameObject.FindGameObjectWithTag("moedatxt").GetComponent<Text>();

            //imagem fundo preto
            fundoPreto = GameObject.FindGameObjectWithTag("fundopreto").GetComponent<Image>();

            //eventos

            //pause
            pauseBtn.onClick.AddListener(Pausar);
            pauseBtnPlay.onClick.AddListener(PausarInverse);
            pauseBtnNovamente.onClick.AddListener(Again);
            pauseBtnMenu.onClick.AddListener(GoMenu);
            pauseBtnLoja.onClick.AddListener(GoLoja);

            //lose
            loseBtnMenu.onClick.AddListener(GoMenu);
            loseBtnNovamente.onClick.AddListener(Again);

            //win
            winBtnMenu.onClick.AddListener(GoMenu);
            winBtnNovamente.onClick.AddListener(Again);
            WinBtnProximo.onClick.AddListener(ProximaFase);
        }
    }

    //metodos pause
    public void Pausar() {
        GAME_MANAGER.instance.pausado = true;
        Time.timeScale = 0;
        fundoPreto.enabled = true;
        painelPause.Play("MenuPause_Anim");
    }

    public void PausarInverse()
    {
        GAME_MANAGER.instance.pausado = false;
        Time.timeScale = 1;
        fundoPreto.enabled = false;
        painelPause.Play("MenuPause_Anim_Reverse");
    }

    //metodo jogar novamente
    public void Again() {
        SceneManager.LoadScene(OndeEstou.instance.fase);
        Time.timeScale = 1;
        GAME_MANAGER.instance.pausado = false;
    }

    //metodo pause-menu
    public void GoMenu() {

        if (OndeEstou.instance.faseMestra == "Mestra1")
        {
            SceneManager.LoadScene("Mestra1");   
        }
        else 
        if(OndeEstou.instance.faseMestra == "Mestra2") {
            SceneManager.LoadScene("Mestra2");
        }

        Time.timeScale = 1;
        GAME_MANAGER.instance.pausado = false;
    }

    //metodo pause loja
    public void GoLoja() {
        AUDIO_MANAGER.instance.GetSom(1);
        SceneManager.LoadScene("Loja");
        Time.timeScale = 1;
        GAME_MANAGER.instance.pausado = false;
    }

    //metodo avancar
    public void ProximaFase() {
        if (OndeEstou.instance.faseN == "Level2_Mestra1" || OndeEstou.instance.faseN == "Level2_Mestra2")
        {
            SceneManager.LoadScene("MenuFasesPai");
        }
        else
        {
            SceneManager.LoadScene(OndeEstou.instance.fase + 1);
        }
    }

}
