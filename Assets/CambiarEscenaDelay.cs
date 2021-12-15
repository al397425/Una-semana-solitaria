using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenaDelay : MonoBehaviour
{
   string niv;
   public void CambiarEscena(string nivel){
            niv = nivel;
           StartCoroutine(establecerPosicionJugador());
   }

   IEnumerator establecerPosicionJugador(){
       yield return new WaitForSeconds(0.27f);
       SceneManager.LoadScene(niv);
   }
}
