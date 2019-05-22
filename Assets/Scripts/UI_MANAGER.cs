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
    [SerializeField]
    private Animator estrela1, estrela2, estrela3;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Carrega(Scene cena, LoadSceneMode modo) {
        //Painel
        painelGameOver = GameObject.Find("MenuLose").GetComponent<Animator>();
        painelWin      = GameObject.Find("MenuWin").GetComponent<Animator>();
        painelPause    = GameObject.Find("Painel_Pause").GetComponent<Animator>();

        //btnwin

    }

}
