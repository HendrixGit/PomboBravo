using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExibePerdeMoedas : MonoBehaviour
{
    [SerializeField]
    private Text textMoedas;
    private int  val;
    [SerializeField]
    private Button btnComprar;

    // Start is called before the first frame update
    void Start()
    {
        textMoedas = GetComponentInChildren<Text>();
        val        = SCORE_MANAGER.instance.LoadMoedas();
        textMoedas.text = val.ToString();
        btnComprar.onClick.AddListener(PerdeMoedas);
    }

    // Update is called once per frame
    void Update()
    {
        if (val >= 50)
        {
            btnComprar.enabled = true;
        }
        else {
            btnComprar.enabled = false;
        }
    }

    void PerdeMoedas() {
        SCORE_MANAGER.instance.PerdeMoedas(50);
        textMoedas.text = SCORE_MANAGER.instance.LoadMoedas().ToString();
    }
}
