using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosMinAzulCla : MonoBehaviour
{
   public GameObject ObjPuntos;
    

    public void OnMouseDown()
    {
        ObjPuntos.GetComponent<Puntos>().puntos += 3;
    }
}
