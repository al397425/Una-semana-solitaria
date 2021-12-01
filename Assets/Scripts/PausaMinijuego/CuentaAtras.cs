using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CuentaAtras : MonoBehaviour
{
	//Tiempo que tarda la cuenta atras
	public float tiempo = 3.0f;
	//Evento al ejecutar cuando acabe la cuenta atras
	public UnityEvent eventoAlTerminarCuentaAtras;
	//Texto de la ui que se modificara
	public TextMeshProUGUI textoUI;
	
	float tiempoActual;
	
	void Start(){
		tiempoActual = tiempo;
		textoUI.text = tiempoActual.ToString("0");
	}
	
    // Update is called once per frame
    void Update()
    {
		tiempoActual -= 1 * Time.unscaledDeltaTime;
        textoUI.text = tiempoActual.ToString("0");
		if(tiempoActual<0){
			eventoAlTerminarCuentaAtras.Invoke();
		}
    }
}
