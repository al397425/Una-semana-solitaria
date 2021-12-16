using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using Cinemachine;

public class GestorEstadoJuego : MonoBehaviour
{
    int estadoJuego;
    public GameObject gestorDeSecuencias;
    GestorSecuencias gs;
    public GameObject jugador;
    public GameObject postMinijuegoTuberias;
    public PolygonCollider2D confinadorCamaraTuberias;//Provisional
    public CinemachineConfiner  camaraCineMachine;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("estado"));
        estadoJuego =PlayerPrefs.GetInt("estado");
        gs = gestorDeSecuencias.GetComponent<GestorSecuencias>();
        
        switch(estadoJuego){
            case 0: 
                gs.EstablecerSecuenciaDirecto(0, true);
            break;

            case 1: 
                gs.EstablecerSecuenciaDirecto(0, false);
                gs.EstablecerSecuenciaDirecto(1, true);
            break;

            case 2: 
                gs.EstablecerSecuenciaDirecto(3, false);
                gs.EstablecerSecuenciaDirecto(4, false);
                gs.EstablecerSecuenciaDirecto(5, true);
                gs.EstablecerSecuenciaDirecto(6, true);
                //Establece la posicion del jugador fuera de la casa de conchi
                jugador.transform.position = postMinijuegoTuberias.transform.position;
                camaraCineMachine.m_BoundingShape2D = confinadorCamaraTuberias;
            break;

            
        }
    }

}
