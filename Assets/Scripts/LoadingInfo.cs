﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingInfo : MonoBehaviour
{
    public Text carregando; 

    public void BtnClick(string s) {
        StartCoroutine(LoadGameProg(s));
    }

    IEnumerator LoadGameProg(string val) {
        AsyncOperation async = SceneManager.LoadSceneAsync(val);
        while (!async.isDone) {
            if (OndeEstou.instance.fase == 0 || OndeEstou.instance.fase == 5)
            {
                carregando.enabled = true;
            }
            yield return null;
        }
    }

    public void Site()
    {
        Application.OpenURL("www.google.com");
    }
}
