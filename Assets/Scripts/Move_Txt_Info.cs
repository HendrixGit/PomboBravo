using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Txt_Info : MonoBehaviour
{
    private Vector3 pos;
    private RectTransform rt;
    private bool libera = false;
    private GameObject buttonBlock, nomeGame;

    private RectTransform uiRect1, uiRect2;

    void Awake()
    {
        uiRect1 = GameObject.FindGameObjectWithTag("canvasback").GetComponent<RectTransform>();
        uiRect2 = GameObject.FindGameObjectWithTag("infotext").GetComponent<RectTransform>();
        buttonBlock = GameObject.FindGameObjectWithTag("btnblock");
        nomeGame    = GameObject.FindGameObjectWithTag("nomegame");
        buttonBlock.SetActive(false);
        rt  = GetComponent<RectTransform>();
        pos = rt.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (libera == true)
        {
            transform.Translate(0, 1 * Time.deltaTime, 0);
            buttonBlock.SetActive(true);
        }
        else {
            rt.anchoredPosition = pos;
            buttonBlock.SetActive(false);
        }

        if (!RectOverLap(uiRect1, uiRect2)) {
            rt.anchoredPosition = pos;
        }

    }

    public void LiberaMov() {
        libera = true;
        nomeGame.SetActive(false);
        BTN_Confs.instance.liga = false;
        BTN_Confs.instance.animaConf.Play("Conf_Anim_Reverse");
        BTN_Confs.instance.animaEngrenagem.Play("Anima_Engrenagem_Reverse");
    }

    public void BlockMov() {
        libera = false;
        nomeGame.SetActive(true);
    }

    bool RectOverLap(RectTransform rectTrans1, RectTransform rectTrans2) {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);
        return rect1.Overlaps(rect2);//sabe se o texto está sobre o canvas
    }
}
