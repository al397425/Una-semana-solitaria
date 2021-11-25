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
    public GameObject FondoReset;
    public GameObject Puntos;
    public GameObject Continuar;
    public GameObject ContinuarTXT;
    public GameObject MineralAmarillo;
    public GameObject MineralRosa;
    public GameObject MineralRojo;
    public GameObject MineralVerde;
    public GameObject MineralAzulOsc;
    public GameObject MineralAzulClaro;
    

     public void Start(){
        FondoReset.gameObject.SetActive(false);
        Reiniciar.gameObject.SetActive(false);
        ReiniciarTXT.gameObject.SetActive(false);
        Continuar.gameObject.SetActive(false);
        MineralAmarillo.gameObject.SetActive(false);
        MineralRojo.gameObject.SetActive(false);
        MineralRosa.gameObject.SetActive(false);
        MineralVerde.gameObject.SetActive(false);
        MineralAzulClaro.gameObject.SetActive(false);
        MineralAzulOsc.gameObject.SetActive(false);
        ContinuarTXT.gameObject.SetActive(false);
    }
    
    public void Update(){
        tiempo -= Time.deltaTime;
        tiempoText.text = "" + tiempo.ToString("f0");

        if (tiempo <= 0){
            Destroy(gameObject);
            FondoReset.gameObject.SetActive(true);
            if(Puntos.GetComponent<Puntos>().puntos < 60){
                Reiniciar.gameObject.SetActive(true);
                ReiniciarTXT.gameObject.SetActive(true);
            }

            else{
                Continuar.gameObject.SetActive(true);
                MineralAmarillo.gameObject.SetActive(true);
                MineralRojo.gameObject.SetActive(true);
                MineralRosa.gameObject.SetActive(true);
                MineralVerde.gameObject.SetActive(true);
                MineralAzulClaro.gameObject.SetActive(true);
                MineralAzulOsc.gameObject.SetActive(true);
                ContinuarTXT.gameObject.SetActive(true);
            }
            
            
        }

    }
  
}
