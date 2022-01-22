using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClickToDestroy : MonoBehaviour
{
    public GameObject ObjPuntos;
    public AudioSource audioDataCoin;
    private bool minado = false;

    public void OnMouseDown()
    {   if (minado == false)
        {
            ObjPuntos.GetComponent<Puntos>().puntos += 1;
            if (!audioDataCoin.isPlaying)
                audioDataCoin.Play(0);
            Destroy(gameObject);
            minado = true;
        }

       
    }

    

}
