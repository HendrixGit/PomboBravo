using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour{

    void Update()
    {
        this.transform.Translate(Input.acceleration.x, Input.acceleration.y, 0);
    }
}
