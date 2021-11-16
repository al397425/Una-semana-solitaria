using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosMinRojos : MonoBehaviour
{
    public GameObject ObjPuntos;
    

    public void OnMouseDown()
    {
        ObjPuntos.GetComponent<Puntos>().puntos += 4;
    }


}
