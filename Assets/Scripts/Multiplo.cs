using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplo : MonoBehaviour
{

    private Vector3 start;
    public  Rigidbody2D pass1, pass2, passaroRB, passPrefab;
    private bool libera = false;
    public int trava = 0;
    private Touch touch;
    private TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        passaroRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOuse
        //if (Input.GetMouseButtonDown(0) && passaroRB.isKinematic == false && trava == 0) {
        //    libera = true;
        //    start = transform.position;
        //    pass1 = Instantiate(passPrefab, new Vector3(start.x, start.y + 0.1f, start.z), Quaternion.identity);
        //    pass2 = Instantiate(passPrefab, new Vector3(start.x, start.y - 0.1f, start.z), Quaternion.identity);
        //    trava = 1;
        //}

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended && trava < 2 && passaroRB.isKinematic == false)
            {
                trava++;
                if (trava == 2)
                {
                    start = transform.position;
                    pass1 = Instantiate(passPrefab, new Vector3(start.x, start.y + 0.1f, start.z), Quaternion.identity);
                    pass2 = Instantiate(passPrefab, new Vector3(start.x, start.y - 0.1f, start.z), Quaternion.identity);
                    libera = true;
                }
            }
        }

    }

    void FixedUpdate()
    {
        if (libera) {
            pass1.velocity     = passaroRB.velocity * 1.6f;
            pass2.velocity     = passaroRB.velocity * 1.1f;
            passaroRB.velocity = passaroRB.velocity * 1.4f;
            libera = false;
        }
    }
}
