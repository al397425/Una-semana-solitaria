using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFly : MonoBehaviour
{
    public float TiempoLimiteSegundos = 20f;
    public Text texto;
    public float tiempoTranscurrido;
    public GameObject matamoscas;
    bool MostrarFinal;
    // Start is called before the first frame update
    void Start()
    {
        tiempoTranscurrido = 0f;
        texto.text = string.Format("Segundos restantes: {0:F0}", TiempoLimiteSegundos);
        MostrarFinal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempoTranscurrido > TiempoLimiteSegundos && !MostrarFinal){
            texto.text = string.Format("Segundos restantes: {0:F0}\nFin del juego ¡Has perdido!", TiempoLimiteSegundos - tiempoTranscurrido);
        }
        else if(!MostrarFinal){
            tiempoTranscurrido += Time.deltaTime;
            texto.text = string.Format("Segundos restantes: {0:F0}\nMoscas cazadas: {1:F0}", TiempoLimiteSegundos - tiempoTranscurrido, matamoscas.GetComponent<PlayerMovement>().MoscasCazadas);
        }
        else{
            
        }
    }
    public void FinalizarPartida(){
        MostrarFinal = true;
    }
}
