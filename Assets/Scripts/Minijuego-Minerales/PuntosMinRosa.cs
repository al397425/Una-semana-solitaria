using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosMinRosa : MonoBehaviour
{
    public GameObject ObjPuntos;
    

    public void OnMouseDown()
    {
        ObjPuntos.GetComponent<Puntos>().puntos += 5;
    }
}
