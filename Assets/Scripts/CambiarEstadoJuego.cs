using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarEstadoJuego : MonoBehaviour
{
    public int estado = 69;
   void Start(){
        PlayerPrefs.SetInt("estado", estado);
    }
}
