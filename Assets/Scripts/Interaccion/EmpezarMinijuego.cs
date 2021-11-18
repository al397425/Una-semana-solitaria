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
	
	void Awake(){
		interfazNoDisponeObjeto = transform.Find("InterfazNoTieneObjeto").gameObject;
	}
	
    void OnTriggerStay2D(Collider2D other){
		if (activo == false && Input.GetKeyDown(teclaDeInteraccion) && other.tag == "Player"){	
			if((nombreObjetoRequerido == "" || other.gameObject.GetComponent<Inventario>().BuscarEliminarObjeto(nombreObjetoRequerido))){
				activo = true;
				if(minijuegoEnOtraEscena == true){
					SceneManager.LoadScene(nombreDelMapa);
				}else{
					Instantiate(objetoMinijuego, new Vector2(0,0), Quaternion.identity);
				}
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
}