using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickToDestroy : MonoBehaviour
{
    public GameObject ObjPuntos;


    void OnMouseDown()
    {
        ObjPuntos.GetComponent<Puntos>().puntos += 10;
        Destroy(gameObject);
    }

}
