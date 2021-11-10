using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrearPuzzleActivador : MonoBehaviour
{
	//Referencia al sistema de dialogos empleado
	//Tecla de interaccion
	public KeyCode teclaDeInteraccion = KeyCode.Space;
	public GameObject puzzleTuberia;
	public int ancho;
	public int alto;
	[Min(1)]
	public int filaPuntoInicio;
	[Min(1)]
	public int filaPuntoFinal;
	//Especificado en segundos
	public float delayFlujoTuberia;
	
	//Evento a llamar
	public UnityEvent eventoAlEmpezarElMinijuego;
	//Evento a llamar
	public UnityEvent eventoAlGanarElMinijuego;
		//Evento a llamar
	public UnityEvent eventoAlPerderElMinijuego;
	
	GameObject puzzle;
	bool puzzleActivado = false; 
	bool resuelto = false;
	
	void OnTriggerStay2D(Collider2D other){
		if (Input.GetKeyDown(teclaDeInteraccion) && puzzleActivado == false)
        {
			puzzleActivado = true;
			if(eventoAlEmpezarElMinijuego != null){
				eventoAlEmpezarElMinijuego.Invoke();
			}
			
			puzzle = (GameObject)Instantiate(puzzleTuberia, new Vector2(0,0), Quaternion.identity);
			puzzle.GetComponent<CrearPuzzle>().iniciarMinijuego(ancho, alto, filaPuntoInicio, filaPuntoFinal, delayFlujoTuberia, gameObject);
		}
	}
	
	public bool Getresuelto(){
		return resuelto;
	}
	
	public void Setresuelto(bool valor){
		resuelto = valor;
	}
}
