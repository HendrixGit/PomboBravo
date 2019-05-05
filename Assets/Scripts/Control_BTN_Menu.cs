using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_BTN_Menu : MonoBehaviour
{
    public Animator btnAnim;
    private bool chave = false;

    public void EventoCliqueMaisJogos() {
        chave = !chave;
        if (chave == true)
        {
            btnAnim.Play("BTN_MAISGAMES_ANIM");
        }
        else {
            btnAnim.Play("BTN_MAISGAMES_ANIM_INVERSE");
        }
    }
}
