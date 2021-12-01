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
	//Numero de huecos en el mapa
	public int numeroDeHuecos = 0;
	//Especificado en segundos
	public float delayFlujoTuberia;
	
	//Evento a llamar
	public UnityEvent eventoAlEmpezarElMinijuego;
	//Evento a llamar
	public UnityEvent eventoAlGanarElMinijuego;
	//Evento a llamar
	public UnityEvent eventoAlPerderElMinijuego;

	//Nombre del objeto a buscar, si es nulo se activara el minijuego igualmente
	public string nombreObjetoRequerido;

	GameObject interfazNoDisponeObjeto;
	GameObject puzzle;
	bool puzzleActivado = false; 
	bool resuelto = false;
	
	void Awake(){
		interfazNoDisponeObjeto = transform.Find("InterfazNoTieneObjeto").gameObject;
	}
	
	void OnTriggerStay2D(Collider2D other){
		if ((Input.GetKeyDown(teclaDeInteraccion) || teclaDeInteraccion == KeyCode.None) && puzzleActivado == false && resuelto == false)
        {
			if((nombreObjetoRequerido == "" || other.gameObject.GetComponent<Inventario>().BuscarEliminarObjeto(nombreObjetoRequerido))){
				puzzleActivado = true;
				if(eventoAlEmpezarElMinijuego != null){
					eventoAlEmpezarElMinijuego.Invoke();
				}
				
				puzzle = (GameObject)Instantiate(puzzleTuberia, new Vector2(0,0), Quaternion.identity);
				puzzle.GetComponent<CrearPuzzle>().iniciarMinijuego(ancho, alto, filaPuntoInicio, filaPuntoFinal, delayFlujoTuberia, gameObject, numeroDeHuecos);
			}else{
				interfazNoDisponeObjeto.GetComponent<Animator>().SetFloat("VelocidadAnimacion", 1);
				interfazNoDisponeObjeto.GetComponent<Animator>().Play("ObjetoNoEncontrado");
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
			//Desactiva la interfaz que indica que no dispone de objeto
			interfazNoDisponeObjeto.GetComponent<Animator>().SetFloat("VelocidadAnimacion", -1);
			interfazNoDisponeObjeto.GetComponent<Animator>().Play("ObjetoNoEncontrado");
		}
	}
	
	public bool Getresuelto(){
		return resuelto;
	}
	
	public void Setresuelto(bool valor){
		resuelto = valor;
	}
	
	public void SetpuzzleActivado(bool valor){
		puzzleActivado = valor;
	}
}
