using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "obstaculo-pescar":
                this.gameObject.SetActive(false);
                break;
        }
        
    }
}
