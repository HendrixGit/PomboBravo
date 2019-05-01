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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (umClick == false)
            {
                umClick = true;
                tempoParaDuploClick = Time.time;
            }
            else {
                umClick    = false;
                liberaZoom = true;
            }
        }
        if (umClick == true) {
            if (Time.time - tempoParaDuploClick > Delay) {
                umClick = false;
            }
        }
        if (Camera.main.orthographicSize > 5 && trava == 1)
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
        else if (Camera.main.orthographicSize < 10 && trava == 2) {
            if (liberaZoom == true)
            {
                Camera.main.orthographicSize += orthoZoomSpeed;
                if (Camera.main.orthographicSize == 10)
                {
                    liberaZoom = false;
                    trava = 1;
                }
            }
        }
    }
}
