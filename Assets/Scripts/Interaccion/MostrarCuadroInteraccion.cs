using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MostrarCuadroInteraccion : MonoBehaviour
{
 	GameObject interfazInteraccion;
	public TextMeshProUGUI textDisplay;
	
	void Awake(){
		interfazInteraccion = transform.Find("InterfazInteraccion").gameObject;
	}
	
	public void EstableceTextoCuandro(string txt){
		transform.Find("InterfazInteraccion/texto").gameObject.GetComponent<TextMeshProUGUI>().SetText(txt);
	}
	
    void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player"){
			interfazInteraccion.GetComponent<Animator>().SetFloat("VelocidadAnimacion", 1);
			interfazInteraccion.GetComponent<Animator>().Play("CuadroInteraccion");
			transform.Find("Borde").gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
			//Activa el outline y la animacion de aparecer con que tecla aparece
			interfazInteraccion.GetComponent<Animator>().SetFloat("VelocidadAnimacion", -1);
			interfazInteraccion.GetComponent<Animator>().Play("CuadroInteraccion");
			transform.Find("Borde").gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
