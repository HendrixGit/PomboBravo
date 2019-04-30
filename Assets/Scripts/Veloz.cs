using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veloz : MonoBehaviour
{
    public Rigidbody2D passaroRB;
    public bool libera = false;
    public int trava;
    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        passaroRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {        
        //if (Input.GetMouseButton(0) && passaroRB.isKinematic == false && trava == 0) {
        //    libera = true;
        //    trava  = 1;
        //}

        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended && trava < 2 && passaroRB.isKinematic == false) {
                trava++;
                if (trava == 2) {
                    libera = true;
                }
            }
        }

    }

    void FixedUpdate()
    {
        if (libera == true) {
            passaroRB.velocity = passaroRB.velocity * 2.5f;
            libera = false;
        }
    }

}
