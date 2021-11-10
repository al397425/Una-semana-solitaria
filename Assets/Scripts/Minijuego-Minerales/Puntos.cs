using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntos : MonoBehaviour
{
    public int puntos;
    public Text TXTpuntos;

    private void Update()
    {
        TXTpuntos.text = "Puntos: " + puntos.ToString();
    }
}
