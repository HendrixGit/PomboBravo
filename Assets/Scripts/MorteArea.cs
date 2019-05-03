using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("pass")){
            Destroy(col.gameObject);
        }
    }
}
