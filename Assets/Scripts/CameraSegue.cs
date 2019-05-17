using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSegue : MonoBehaviour
{
    private float t = 1;

    void Update()
    {
        if (GAME_MANAGER.instance.jogoComecou) {
            if (transform.position.x != GAME_MANAGER.instance.objE.position.x && GAME_MANAGER.instance.passaroLancado == false)
            {
                t -= 0.5f * Time.deltaTime;

                transform.position = new Vector3(Mathf.SmoothStep(GAME_MANAGER.instance.objE.position.x, Camera.main.transform.position.x, t), transform.position.y, transform.position.z);
            }
            else {
                t = 1;
            }
        }
    }
}
