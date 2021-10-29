using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TipoTuberia : MonoBehaviour
{
		
	//Tipos de tuberias
	[System.Serializable] public struct TiposTuberiasEditor{
		public List<EnumTuberias.Tuberia> tuberiasCompatibles;
		public EnumTuberias.Tuberia tipoDeTuberia;
		public Sprite imagenTuberia;
	}
	public List<TiposTuberiasEditor> tiposTuberia;

	List<EnumTuberias.Tuberia> tuberiasCompatibles;
	
	EnumTuberias.Tuberia tipoTuberia;
	
	bool puedeArrastrar = true;
	float delayFlujo =5.0f;
	
    // Start is called before the first frame update
    void Awake()
    {
        //!!!!!!!! PROVISIONAL !!!!!!!!!!!!!!!!!!!
			int indice = Random.Range(0, 6);
			EstablecerTipoTuberia(tiposTuberia[indice].tipoDeTuberia);
		//-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	/** Establece el sprite de la tuberia y la direccion que tendra
	* @note No se usa diccionarios para una busqueda mas rapida ya que unity no deja exponer diccionarios
	* Por lo tanto habria que exponer una lista de estructuras y pasar eso al diccionario con lo que tendria un costo O(N)
	* De esta forma tambien tiene costo O(N)
	**/
	public void EstablecerTipoTuberia(EnumTuberias.Tuberia tuberia){
		for(int i = 0; i < tiposTuberia.Count; i++){
			if(tuberia == tiposTuberia[i].tipoDeTuberia){
				GetComponent<Image> ().sprite = tiposTuberia[i].imagenTuberia;
				tipoTuberia = tiposTuberia[i].tipoDeTuberia;
				tuberiasCompatibles= tiposTuberia[i].tuberiasCompatibles;
				return;
			}
		}
	}	
	
	/**
	 * Activa la tuberia y despues de un tiempo comprueba la siguiente direccion a tomar
	**/
	public IEnumerator ActivarTuberia(GameObject [,]matrizSlots, int fila, int columna){
		yield return new WaitForSeconds(delayFlujo);
		
		GameObject tuberia = matrizSlots[fila,columna].transform.GetChild(0).gameObject;
		
		if(tuberiasCompatibles.Contains(tuberia.GetComponent<TipoTuberia>().tipoTuberia)){
			Debug.Log("Muy bien!");
		}else{
			Debug.Log("nope");
		}
	}
	
	
	/**
	 * Establece el valor si puede arrastrar 
	**/
	public void SetpuedeArrastrar(bool valor){
		puedeArrastrar = valor;
	}
	
	/**
	 * Obtiene el valor si puede arrastrar 
	**/
	public bool GetpuedeArrastrar(){
		return puedeArrastrar;
	}
	
	/**
	 * Establece el valor del delay del flujo
	**/
	public void SetdelayFlujo(float valor){
		delayFlujo = valor;
	}
	
	/**
	 * Obtiene el valor del delay del flujo
	**/
	public float GetdelayFlujo(){
		return delayFlujo;
	}
}
