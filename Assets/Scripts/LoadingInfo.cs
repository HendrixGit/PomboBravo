using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingInfo : MonoBehaviour
{
    public Text carregando; 

    public void BtnClick() {
        StartCoroutine(LoadGameProg());
    }

    IEnumerator LoadGameProg() {
        AsyncOperation async = SceneManager.LoadSceneAsync(6);
        while (!async.isDone) {
            carregando.enabled = true;
            yield return null;
        }
    }
}
