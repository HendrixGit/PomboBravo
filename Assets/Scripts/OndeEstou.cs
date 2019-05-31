using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class OndeEstou : MonoBehaviour
{
    public static OndeEstou instance;
    public int fase = -1;
    public string faseN;

    public string faseMestra;

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
    }

    //mestra
    public void Mestra(string nome) {
        faseMestra = nome;
        SceneManager.LoadScene(faseMestra);
    }


}
