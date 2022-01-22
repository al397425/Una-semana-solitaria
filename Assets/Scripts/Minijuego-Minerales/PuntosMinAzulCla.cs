using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosMinAzulCla : MonoBehaviour
{
   public GameObject ObjPuntos;
   public AudioSource audioDataCoin;
   private bool minado = false;

    public void OnMouseDown()
    {
        if (minado == false)
        {
            if (!audioDataCoin.isPlaying)
                audioDataCoin.Play(0);
            ObjPuntos.GetComponent<Puntos>().puntos += 3;
            minado = true;
        }
    }
}
