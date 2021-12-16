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
    // Start is called before the first frame update
    void Start()
    {
        tiempoTranscurrido = 0f;
        texto.text = string.Format("Segundos restantes: {0:F0}", TiempoLimiteSegundos);
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempoTranscurrido > TiempoLimiteSegundos){
            texto.text = string.Format("Segundos restantes: {0:F0}\nFin del juego ¡Has perdido!", TiempoLimiteSegundos - tiempoTranscurrido);
        }
        else{
            tiempoTranscurrido += Time.deltaTime;
            texto.text = string.Format("Segundos restantes: {0:F0}\nMoscas cazadas: {1:F0}", TiempoLimiteSegundos - tiempoTranscurrido, matamoscas.GetComponent<PlayerMovement>().MoscasCazadas);
        }
    }
}
