using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MostrarCuadroInteraccion : MonoBehaviour
{
 	GameObject interfazInteraccion;
	
	bool cuadroDesactivado = false;

	void Awake(){
		interfazInteraccion = transform.Find("InterfazInteraccion").gameObject;
	}
	
	public void EstableceTextoCuandro(string txt){
		transform.Find("InterfazInteraccion/texto").gameObject.GetComponent<TextMeshProUGUI>().SetText(txt);
	}
	
    void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			EjecutarAnimacion("CuadroInteraccion",1.0f);
			//transform.Find("Borde").gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
			//Activa el outline y la animacion de aparecer con que tecla aparece
			EjecutarAnimacion("CuadroInteraccion",-1.0f, 1.0f);
			//transform.Find("Borde").gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public void EjecutarAnimacion(string nombre, float velocidad, float inicio = 0.0f){
		if(cuadroDesactivado == false){
			interfazInteraccion.GetComponent<Animator>().SetFloat("VelocidadAnimacion", velocidad);
			interfazInteraccion.GetComponent<Animator>().Play(nombre,0,inicio);
		}
	}

	public void SetcuadroDesactivado(bool valor){
		cuadroDesactivado = valor;
	}

	public void ComprobarDesactivarCuadro(){
		if(cuadroDesactivado == true){
			//gameObject.active = false; 
		}
	}
}
