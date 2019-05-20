using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataRastro : MonoBehaviour
{
    void Update()
    {
        MataRastroPassaro();   
    }

    void MataRastroPassaro() {
        if (GAME_MANAGER.instance.passaroLancado) {
            if (transform.parent == null) {
                Destroy(gameObject, 10.0f);
            }
        }
    }
}
