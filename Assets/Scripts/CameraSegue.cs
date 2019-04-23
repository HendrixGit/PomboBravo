using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSegue : MonoBehaviour
{

    [SerializeField]
    private Transform objE, objD, passaro;

    void Update()
    {
        if (passaro != null)
        {
            Vector3 posCamera = transform.position;
            posCamera.x = passaro.position.x;//segue a bola
            posCamera.x = Mathf.Clamp(posCamera.x, objE.position.x, objD.position.x);//limitei ate onde a camera vai
            transform.position = posCamera;
        }
    }
}
