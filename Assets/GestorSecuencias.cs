using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorSecuencias : MonoBehaviour
{
    [System.Serializable]
    public struct Parametros{
       public bool activo;
       public int secuencia;
    }


     [System.Serializable]
    public class ListaSecuencias{
        public GameObject [] secuencias;
    }

   public ListaSecuencias [] lista_secuencias;

    int sec;
    bool act;

    public void EstablecerNumeroSecuencia(int secuencia){
        sec = secuencia;
    }

    public void EstablecerSecuenciaActiva(bool activo){
        act = activo;
    }

    public void EstablecerSecuenciaDirecto(int s, bool a){
        sec = s;
        act = a;
        EstablecerSecuencia();
    }

   public void EstablecerSecuencia(){
       for(int i = 0; i<lista_secuencias[sec].secuencias.Length; i++){
           lista_secuencias[sec].secuencias[i].SetActive(act);
       }
   }
}
