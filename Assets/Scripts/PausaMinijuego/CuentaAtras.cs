using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CuentaAtras : MonoBehaviour
{
	//Tiempo que tarda la cuenta atras
	public int tiempo = 3;
	//Evento al ejecutar cuando acabe la cuenta atras
	public UnityEvent eventoAlTerminarCuentaAtras;
	//Texto de la ui que se modificara
	public TextMeshProUGUI textoUI;
	
	AudioSource[] sonidos;

	float tiempoActual;

	float segundos = 0;
	
	void Start(){
		 sonidos = GetComponents<AudioSource>();
		segundos = tiempo;
		textoUI.text = segundos.ToString();
		StartCoroutine(Contar());
	}

	IEnumerator Contar(){
		while(segundos > 0){
			sonidos[0].Play(0);
			yield return new WaitForSecondsRealtime(1.0f);
			segundos -= 1;
			textoUI.text = segundos.ToString();
		}
		sonidos[1].Play(0);
		yield return new WaitForSecondsRealtime(0.2f);
		eventoAlTerminarCuentaAtras.Invoke();
		
	}
}
