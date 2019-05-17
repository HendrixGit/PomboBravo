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
    public bool win, jogoComecou;
    public string nomePassaro;

    public bool passaroLancado = false;
    public Transform objE, objD;


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
        jogoComecou = false;
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
        if (win)
        {
            WinGame();
        }
        else {
            NascPassaro();
        }
    }
}
