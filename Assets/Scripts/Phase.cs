using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase : MonoBehaviour
{
    public Text txt;
    public Touch toque;

    void Update()
    {
        if (Input.touchCount > 0) {
            toque = Input.GetTouch(0);//pega o primeiro toque na tela
            switch (toque.phase) {
                case TouchPhase.Began: txt.text = "Began"; break;
                case TouchPhase.Ended: txt.text = "Ended"; break;
                case TouchPhase.Moved: txt.text = "Moved"; break;
                //case TouchPhase.Stationary: break; dedo parado na tela
                case TouchPhase.Canceled: txt.text = "Canceled"; break;
            }
        }
    }
}
