using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerObjeto : MonoBehaviour
{
    public GameObject padre;



    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Player-pescar":
                gameObject.transform.parent = padre.transform;
                break;
        }

    }
}
