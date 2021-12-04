using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmpezarMinijuego : MonoBehaviour
{
	//Nombre del objeto a buscar, si es nulo se activara el minijuego igualmente
	public string nombreObjetoRequerido;
		
	//Objeto minijuego por si quiere ser creado
	public GameObject objetoMinijuego;
	
	//Indica si el minijuego esta en una escena aparte y hay que cargarlo
	public bool minijuegoEnOtraEscena=true;
	
	//Nombre del mapa
	public string nombreDelMapa;

	
	//Tecla de interaccion
	public KeyCode teclaDeInteraccion = KeyCode.Space;
	
	bool activo = false;
	
	GameObject interfazNoDisponeObjeto;

	bool dentroTrigger = false;
	bool minijuegoActivado = false;
	
	void Awake(){
		interfazNoDisponeObjeto = transform.Find("InterfazNoTieneObjeto").gameObject;
	}
	
	void Update(){
		if(dentroTrigger == true && Input.GetKeyDown(teclaDeInteraccion)){
			minijuegoActivado = true;
		}
	}

    void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			
			dentroTrigger = true;
			if (activo == false && minijuegoActivado == true){	
				if((nombreObjetoRequerido == "" || other.gameObject.GetComponent<Inventario>().BuscarEliminarObjeto(nombreObjetoRequerido))){
					activo = true;
					if(minijuegoEnOtraEscena == true){
						SceneManager.LoadScene(nombreDelMapa);
					}else{
						Instantiate(objetoMinijuego, new Vector2(0,0), Quaternion.identity);
					}
				}else{
					minijuegoActivado = false;
					interfazNoDisponeObjeto.GetComponent<Animator>().SetFloat("VelocidadAnimacion", 1);
					interfazNoDisponeObjeto.GetComponent<Animator>().Play("ObjetoNoEncontrado");
				}
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
			//Desactiva la interfaz que indica que no dispone de objeto
			interfazNoDisponeObjeto.GetComponent<Animator>().SetFloat("VelocidadAnimacion", -1);
			interfazNoDisponeObjeto.GetComponent<Animator>().Play("ObjetoNoEncontrado");
			//Para evitar darle al espacio y despues al colisionar sin pulsar nada se active
			dentroTrigger = false;
		}
	}
}
