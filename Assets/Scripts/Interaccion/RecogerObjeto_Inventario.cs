using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerObjeto_Inventario : MonoBehaviour
{
	//Tecla de interaccion
	public KeyCode teclaDeInteraccion = KeyCode.Space;
	
	//Objeto interfaz creada
	public GameObject objetoInterfaz;
	
	public string descripcion;

	bool recogerObjeto = false;

	bool objetoRecogido = false;
	
	bool dentroTrigger = false;

	void Awake(){
		GetComponent<MostrarCuadroInteraccion>().EstableceTextoCuandro(teclaDeInteraccion.ToString());
	}

	void Update(){
		if(dentroTrigger == true && Input.GetKeyDown(teclaDeInteraccion)){
			recogerObjeto = true;
		}
	}
	
	//Para evitar darle al espacio y despues al colisionar sin pulsar nada se active
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			dentroTrigger = false;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			dentroTrigger = true;
			if (objetoRecogido == false && recogerObjeto == true){
				other.gameObject.GetComponent<MovementCharacter>().enabled = false;
				other.gameObject.GetComponent<Animator>().enabled = false;
				objetoRecogido = true;
				other.gameObject.GetComponent<Inventario>().AgregarObjeto(gameObject.name, gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite);
				GameObject interfaz = Instantiate(objetoInterfaz, new Vector2(0,0), Quaternion.identity);
				//Establece referencia jugador
				interfaz.GetComponent<EstablezerInterfaz>().Setjugador(other.gameObject);
				interfaz.GetComponent<EstablezerInterfaz>().EstablecerInformacion(gameObject.name,gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite, descripcion);
				Destroy(gameObject);
			}
		}
	}
}
