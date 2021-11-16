using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text tiempoText;
    public float tiempo = 0.0f;

    public void Update(){
        tiempo -= Time.deltaTime;
        tiempoText.text = "" + tiempo.ToString("f0");
    }
}
