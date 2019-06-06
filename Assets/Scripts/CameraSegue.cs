using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSegue : MonoBehaviour
{
    private float t = 1;
    private Transform objE, objD;

    void Awake()
    {
        objE = GameObject.FindGameObjectWithTag("PE").GetComponent<Transform>();
        objD = GameObject.FindGameObjectWithTag("PD").GetComponent<Transform>();
    }

    void Update()
    {
        if (GAME_MANAGER.instance.jogoComecou) {
            if (transform.position.x != objE.position.x && GAME_MANAGER.instance.passaroLancado == false)
            {
                t -= 0.5f * Time.deltaTime;

                transform.position = new Vector3(Mathf.SmoothStep(objE.position.x, Camera.main.transform.position.x, t), transform.position.y, transform.position.z);
            }
            else {
                t = 1;
            }
        }
    }
}
