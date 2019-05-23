using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MANAGER : MonoBehaviour
{
    public static UI_MANAGER instance;
    public Animator painelGameOver, painelWin, painelPause;
    [SerializeField]
    private Button winBtnMenu, winBtnNovamente, WinBtnProximo;
    public Animator estrela1, estrela2, estrela3;
    [SerializeField]
    private Button loseBtnMenu, loseBtnNovamente;
    [SerializeField]
    private Button pauseBtn, pauseBtnPlay, pauseBtnNovamente, pauseBtnMenu, pauseBtnLoja;
    public AudioSource winSom;

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
        //Painel
        painelGameOver = GameObject.Find("MenuLose").GetComponent<Animator>();
        painelWin      = GameObject.Find("MenuWin").GetComponent<Animator>();
        painelPause    = GameObject.Find("Painel_Pause").GetComponent<Animator>();

        //btnwin
        winBtnMenu      = GameObject.Find("Button_Menu").GetComponent<Button>();
        winBtnNovamente = GameObject.Find("Button_Novamente").GetComponent<Button>();
        WinBtnProximo   = GameObject.Find("Button_Avancar").GetComponent<Button>();

        //Estrelas
        estrela1 = GameObject.Find("Estrela1_Win").GetComponent<Animator>();
        estrela2 = GameObject.Find("Estrela2_Win").GetComponent<Animator>();
        estrela3 = GameObject.Find("Estrela3_Win").GetComponent<Animator>();

        //btnLose
        loseBtnMenu      = GameObject.Find("Button_Menu_L").GetComponent<Button>();
        loseBtnNovamente = GameObject.Find("Button_Novamente_L").GetComponent<Button>();

        //btnPause
        pauseBtn          = GameObject.Find("Pause").GetComponent<Button>();
        pauseBtnPlay      = GameObject.Find("play").GetComponent<Button>();
        pauseBtnNovamente = GameObject.Find("again").GetComponent<Button>();
        pauseBtnMenu      = GameObject.Find("scene").GetComponent<Button>();
        pauseBtnLoja      = GameObject.Find("shop").GetComponent<Button>();

        //audio
        winSom = painelWin.GetComponent<AudioSource>();

    }

}
