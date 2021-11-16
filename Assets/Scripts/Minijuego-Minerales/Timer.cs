using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text tiempoText;
    public float tiempo = 0.0f;
    public GameObject Reiniciar;
    public GameObject ReiniciarTXT;

     public void Start(){
        Reiniciar.gameObject.SetActive(false);
        ReiniciarTXT.gameObject.SetActive(false);
    }
    
    public void Update(){
        tiempo -= Time.deltaTime;
        tiempoText.text = "" + tiempo.ToString("f0");

        if (tiempo <= 0){
            Destroy(gameObject);
            Reiniciar.gameObject.SetActive(true);
            ReiniciarTXT.gameObject.SetActive(true);
        }

    }
  
}
