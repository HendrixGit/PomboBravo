using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteWin : MonoBehaviour
{

    public Animator menu, estrela1, estrela2, estrela3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            menu.Play("MenuWin_Anim");
            estrela1.Play("Estrela1_Anim");
            estrela2.Play("Estrela2_Anim");
            estrela3.Play("Estrela3_Anim");
        }

    }
}
