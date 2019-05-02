﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomB : MonoBehaviour
{
    public  float orthoZoomSpeed = 1f;
    public  Camera camera;
    private Touch toque0, toque1;
    private Vector2 touchZeroPrevPos, touchOnePrevPos;
    private float prevTouchDeltaMag, touchDeltaMag, deltaMagnitudeDiff;



    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2) {
            toque0 = Input.GetTouch(0);
            toque1 = Input.GetTouch(1);

            touchZeroPrevPos = toque0.position - toque0.deltaPosition;//Delta positioin vector 2 diferenca entre a posicao de toque mais recente e a anterior
            touchOnePrevPos  = toque1.position - toque1.deltaPosition;

            prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            touchDeltaMag     = (toque0.position - toque1.position).magnitude;

            deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (camera.orthographic) {
                camera.orthographicSize -= deltaMagnitudeDiff * (orthoZoomSpeed * Time.deltaTime);
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 5);
                camera.orthographicSize = Mathf.Min(camera.orthographicSize, 10);
            }
        }
    }
}
