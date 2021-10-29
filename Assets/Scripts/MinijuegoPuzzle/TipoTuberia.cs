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
		//El sentido principal con la logica de que el agua va de izquierda a derecha
		public EnumTuberias.Sentido sentidoPrincipal;
		//El sentido opuesto cuando el agua cambia de direcion y se tiene que hacer una lectirura de derecha a izquierda
		public EnumTuberias.Sentido sentidoSecundario;
	}
	public List<TiposTuberiasEditor> tiposTuberia;

	List<EnumTuberias.Tuberia> tuberiasCompatibles;
	
	EnumTuberias.Tuberia tipoTuberia;
	EnumTuberias.Sentido sentidoPrincipal;
	EnumTuberias.Sentido sentidoSecundario;
	
	bool puedeArrastrar = true;
	bool llenaDeAgua = false;
	
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
				sentidoPrincipal = tiposTuberia[i].sentidoPrincipal;
				sentidoSecundario = tiposTuberia[i].sentidoSecundario;
				return;
			}
		}
	}	
	
	/**
	 * Activa la tuberia y despues de un tiempo comprueba la siguiente direccion a tomar
	**/
	public IEnumerator ActivarTuberia(GameObject [,]matrizSlots, int fila, int columna, int modificacionHorizontal, int modificacionVertical){
		
		yield return new WaitForSeconds(delayFlujo);
		int x=0,y=0;
		llenaDeAgua = true;
		
		GameObject tuberia = matrizSlots[fila,columna].transform.GetChild(0).gameObject;
		tuberia.GetComponent<Image> ().color = new Color32(100,0,0,100);
		if(tuberiasCompatibles.Contains(tuberia.GetComponent<TipoTuberia>().tipoTuberia)){
			GetComponent<Image> ().color = new Color32(0,100,0,100);
			tuberia = matrizSlots[fila-modificacionHorizontal,columna-modificacionVertical].transform.GetChild(0).gameObject;
			if(tuberia.GetComponent<TipoTuberia>().GetllenaDeAgua() == false){
				switch(sentidoPrincipal){
					case EnumTuberias.Sentido.arriba: y=-1;Debug.Log("arriba");break;
					case EnumTuberias.Sentido.abajo: y=1;Debug.Log("abajo");break;
					case EnumTuberias.Sentido.izquierda: x=-1;Debug.Log("izq");break;
					case EnumTuberias.Sentido.derecha: x=1;Debug.Log("der");break;
				}
			}else{
				switch(sentidoPrincipal){
					case EnumTuberias.Sentido.arriba: y=-1;Debug.Log("arriba ALT");break;
					case EnumTuberias.Sentido.abajo: y=1;Debug.Log("abajo ALT");break;
					case EnumTuberias.Sentido.izquierda: x=-1;Debug.Log("izq ALT");break;
					case EnumTuberias.Sentido.derecha: x=1;Debug.Log("der ALT");break;
				}
			}
			
			StartCoroutine(ActivarTuberia(matrizSlots,fila+x,columna+y,x,y));
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
	 * Establece el valor si esta lleno de agua 
	**/
	public void SetllenaDeAgua(bool valor){
		llenaDeAgua = valor;
	}
	
	/**
	 * Obtiene el valor si esta lleno de agua 
	**/
	public bool GetllenaDeAgua(){
		return llenaDeAgua;
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
