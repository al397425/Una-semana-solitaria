using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerObjeto_Inventario : MonoBehaviour
{
	//Tecla de interaccion
	public KeyCode teclaDeInteraccion = KeyCode.Space;
	
	void Awake(){
		GetComponent<MostrarCuadroInteraccion>().EstableceTextoCuandro(teclaDeInteraccion.ToString());
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (Input.GetKeyDown(teclaDeInteraccion) && other.tag == "Player"){
			other.gameObject.GetComponent<Inventario>().AgregarObjeto(gameObject.name, gameObject.GetComponent<SpriteRenderer>().sprite);
			Destroy(gameObject);
		}
	}
}
