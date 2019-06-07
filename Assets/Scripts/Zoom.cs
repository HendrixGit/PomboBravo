using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float orthoZoomSpeed = 0.5f;
    public bool liberaZoom;
    public int trava = 1;

    public bool umClick = false;
    public float tempoParaDuploClick;
    public float Delay;

    // Update is called once per frame
    void Update()
    {
        if (GAME_MANAGER.instance.jogoComecou && !GAME_MANAGER.instance.pausado) {

            if (Input.GetMouseButtonDown(0)) {
                if (umClick == false)
                {
                    umClick = true;
                    tempoParaDuploClick = Time.time;
                }
                else {
                    umClick = false;
                    liberaZoom = true;
                }
            }
            if (umClick == true) {
                if (Time.time - tempoParaDuploClick > Delay) {
                    umClick = false;
                }
            }

        }

        if (Camera.main.orthographicSize > CameraSizeHandler.camSize/2 && trava == 1)
        {
            if (liberaZoom == true)
            {
                Camera.main.orthographicSize -= orthoZoomSpeed;
                if (Camera.main.orthographicSize == 5)
                {
                    liberaZoom = false;
                    trava = 2;
                }
            }
        }
        else if (Camera.main.orthographicSize < CameraSizeHandler.camSize / 2 && trava == 2) {
            if (liberaZoom == true)
            {
                Camera.main.orthographicSize += orthoZoomSpeed;
                if (Camera.main.orthographicSize == CameraSizeHandler.camSize)
                {
                    liberaZoom = false;
                    trava = 1;
                }
            }
        }
    }
}
