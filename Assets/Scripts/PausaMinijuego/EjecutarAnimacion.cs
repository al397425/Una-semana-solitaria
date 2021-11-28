using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjecutarAnimacion : MonoBehaviour
{
	//Objeto para animar
    public GameObject objetoParaAnimar;
	public string nombreAnimacion = "AparecerInstruccion";
	
	void Start(){
		objetoParaAnimar.GetComponent<Animator>().Play(nombreAnimacion,-1,0);
	}
}
