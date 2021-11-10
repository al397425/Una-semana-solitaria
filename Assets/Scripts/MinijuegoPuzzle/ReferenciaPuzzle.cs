using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenciaPuzzle : MonoBehaviour
{
	GameObject tabl;
	GameObject rafActivador;
	
	public void SetRefTablero(GameObject tab){
		tabl = tab;
	}
	
	public void SetRefActivador(GameObject activador){
		rafActivador = activador;
	}
	
	public void DestruirElPuzzle(GameObject pantallaFinal){
		//Siempre ejecuta un evento que es que establece como completado el puzzlew
		if(tabl.GetComponent<DestruirPuzzle>().eventoAlDestruirElMinijuego != null){
			tabl.GetComponent<DestruirPuzzle>().eventoAlDestruirElMinijuego.Invoke();
		}
		
		DestroyImmediate(pantallaFinal, true);
		DestroyImmediate(tabl, true);
	}
	
	public void ReactivarYDestruirElPuzzle(GameObject pantallaFinal){
		//Reactiva el puzzlew
		rafActivador.GetComponent<CrearPuzzleActivador>().SetpuzzleActivado(false);
		//Siempre ejecuta un evento que es que establece como completado el puzzlew
		if(tabl.GetComponent<DestruirPuzzle>().eventoAlDestruirElMinijuego != null){
			tabl.GetComponent<DestruirPuzzle>().eventoAlDestruirElMinijuego.Invoke();
		}
		
		DestroyImmediate(pantallaFinal, true);
		DestroyImmediate(tabl, true);
	}
	
	public void ReiniciarPuzzle(GameObject nuevoPuzzleACrear){
		GameObject puzzle = (GameObject)Instantiate(nuevoPuzzleACrear, new Vector2(0,0), Quaternion.identity);
		puzzle.GetComponent<CrearPuzzle>().iniciarMinijuego(rafActivador.GetComponent<CrearPuzzleActivador>().ancho, rafActivador.GetComponent<CrearPuzzleActivador>().alto, rafActivador.GetComponent<CrearPuzzleActivador>().filaPuntoInicio, rafActivador.GetComponent<CrearPuzzleActivador>().filaPuntoFinal, rafActivador.GetComponent<CrearPuzzleActivador>().delayFlujoTuberia, rafActivador, rafActivador.GetComponent<CrearPuzzleActivador>().numeroDeHuecos);
		DestroyImmediate(gameObject, true);
		DestroyImmediate(tabl, true);
	}
}
