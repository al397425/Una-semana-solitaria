using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenciaPuzzle : MonoBehaviour
{
	GameObject tabl;
	public void SetRefTablero(GameObject tab){
		tabl = tab;
	}
	
	public void DestruirElPuzzle(GameObject pantallaFinal){
		//Siempre ejecuta un evento que es que establece como completado el puzzlew
		if(tabl.GetComponent<DestruirPuzzle>().eventoAlDestruirElMinijuego != null){
			tabl.GetComponent<DestruirPuzzle>().eventoAlDestruirElMinijuego.Invoke();
		}
		
		DestroyImmediate(pantallaFinal, true);
		DestroyImmediate(tabl, true);
	}
}
