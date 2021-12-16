using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPrefab : MonoBehaviour
{
    public GameObject objeto;
    public GameObject jugador;

    public void ActivaPrefab(bool activo){
        objeto.SetActive(activo);
        jugador.GetComponent<MovementCharacter>().enabled=false;
    }
}
