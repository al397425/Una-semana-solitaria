using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosMinAzulOsc : MonoBehaviour
{

    public GameObject ObjPuntos;
    

    public void OnMouseDown()
    {
        ObjPuntos.GetComponent<Puntos>().puntos += 6;
    }

}
