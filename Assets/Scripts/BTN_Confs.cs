using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_Confs : MonoBehaviour
{
    public static BTN_Confs instance;
    public bool liga = false;
    public Animator animaConf, animaEngrenagem;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    public void ClickBTN() {
        liga = !liga;

        if (liga)
        {
            animaConf.Play("Conf_Anim");
            animaEngrenagem.Play("Anima_Engrenagem");
        }
        else {
            animaConf.Play("Conf_Anim_Reverse");
            animaEngrenagem.Play("Anima_Engrenagem_Reverse");
        }
    }
}
