using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickToDestroy : MonoBehaviour
{
    public GameObject ObjPuntos;
    

    public void OnMouseDown()
    {
        ObjPuntos.GetComponent<Puntos>().puntos += 10;
        Destroy(gameObject);
    }

}
