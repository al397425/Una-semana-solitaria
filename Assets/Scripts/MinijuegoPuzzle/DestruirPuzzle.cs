using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestruirPuzzle : MonoBehaviour
{
	//Eventos al destruir el puzzle como hacer que el jugador pueda volver a caminar
	public UnityEvent eventoAlDestruirElMinijuego;
	
	GameObject refTablero;
		
    public void DestruirElPuzzle(GameObject pantallaFinal){
		//Siempre ejecuta un evento que es que establece como completado el puzzlew
		if(eventoAlDestruirElMinijuego != null){
			eventoAlDestruirElMinijuego.Invoke();
		}
		DestroyImmediate(pantallaFinal, true);
		DestroyImmediate(refTablero, true);
	}
	
	public void SetrefTablero(GameObject tablero){
		refTablero = tablero;
	}
}
