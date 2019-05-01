using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteTudo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Morte());
    }

    IEnumerator Morte() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
