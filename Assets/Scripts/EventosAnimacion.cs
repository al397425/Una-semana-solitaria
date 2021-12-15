using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventosAnimacion : MonoBehaviour
{
    public UnityEvent eventos;
    public void EjecutarEventos(){
        if(eventos != null)
            eventos.Invoke();
    }
}
