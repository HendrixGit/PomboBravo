using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class OndeEstou : MonoBehaviour
{
    public static OndeEstou instance;
    public int fase = -1;
    public string faseN;

    public string faseMestra;

    public Button btnM1, btnM2;
    [SerializeField]
    private GameObject UIManagerGO, GameManagerGO;

    void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += VerificaFase;
    }

    public void VerificaFase(Scene cena, LoadSceneMode modo) {
        fase  = SceneManager.GetActiveScene().buildIndex;
        faseN = SceneManager.GetActiveScene().name;

        if (faseN == "MenuFasesPai") {
            btnM1 = GameObject.Find("ButtonFase").GetComponent<Button>();
            btnM2 = GameObject.Find("ButtonFase2").GetComponent<Button>();

            btnM1.onClick.AddListener(() => Mestra("Mestra1"));
            btnM2.onClick.AddListener(() => Mestra("Mestra2"));
        }


        if (fase >= 1 && fase <= 4)
        {
            Instantiate(UIManagerGO);
            Instantiate(GameManagerGO);
        }


    }

    //mestra
    public void Mestra(string nome) {
        faseMestra = nome;
        SceneManager.LoadScene(faseMestra);
    }


}
