using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosMinAzulOsc : MonoBehaviour
{

    public GameObject ObjPuntos;
    public AudioSource audioDataCoin;


    public void OnMouseDown()
    {
        if (!audioDataCoin.isPlaying)
            audioDataCoin.Play(0);
        ObjPuntos.GetComponent<Puntos>().puntos += 6;
    }

}
