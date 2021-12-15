using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transicion : MonoBehaviour
{
    GameObject jugador;
    
    Animator anim;
    public GameObject nuevaPosicionJugador;
    public GameObject objetoTransicion;
    public float delay = 0.5f;

   void OnTriggerEnter2D(Collider2D other){
       if (other.tag == "Player"){
           objetoTransicion.GetComponent<EjecutarFundido>().EjecutarFadeIn();
           jugador = other.gameObject;
           jugador.GetComponent<MovementCharacter>().enabled = false;
           StartCoroutine(establecerPosicionJugador());
       }
   }

   IEnumerator establecerPosicionJugador(){
       yield return new WaitForSeconds(delay);
       jugador.transform.position = nuevaPosicionJugador.transform.position;
       objetoTransicion.GetComponent<EjecutarFundido>().EjecutarFadeOut();
        jugador.GetComponent<MovementCharacter>().enabled = true;
   }
}
