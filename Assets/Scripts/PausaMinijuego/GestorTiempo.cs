using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorTiempo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PararTiempo();
    }
	
	//Para el tiempo y ademas como subproducto se desactiva los inputs
	public void PararTiempo(){
		Time.timeScale = 0.0f;
	}
	
	//Reanuda el tiempo y ademas como subproducto se activa los inputs
	public void ReanudarTiempo(){
		Time.timeScale = 1.0f;
	}
}
