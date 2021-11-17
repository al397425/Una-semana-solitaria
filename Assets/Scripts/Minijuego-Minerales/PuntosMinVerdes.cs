using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosMinVerdes : MonoBehaviour
{
    public GameObject ObjPuntos;
    

    public void OnMouseDown()
    {
        ObjPuntos.GetComponent<Puntos>().puntos += 2;
    }
}
