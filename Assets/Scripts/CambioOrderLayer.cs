using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioOrderLayer : MonoBehaviour
{
    public SpriteRenderer spr;
    public int ordenACambiar;
    public int ordenOriginal;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
            spr.sortingOrder = ordenACambiar;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            spr.sortingOrder = ordenOriginal;
        }
    }
}
