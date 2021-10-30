using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TipoTuberia : MonoBehaviour
{
		
	//Tipos de tuberias
	[System.Serializable] public struct TiposTuberiasEditor{
		public List<EnumTuberias.Tuberia> tuberiasCompatiblesSentido1;
		public List<EnumTuberias.Tuberia> tuberiasCompatiblesSentido2;
		public EnumTuberias.Tuberia tipoDeTuberia;
		public Sprite imagenTuberia;
		//El sentido principal con la logica de que el agua va de izquierda a derecha
		public EnumTuberias.Sentido sentidoHorizontal;
		//El sentido opuesto cuando el agua cambia de direcion y se tiene que hacer una lectirura de derecha a izquierda
		public EnumTuberias.Sentido sentidoVertical;
	}
	public List<TiposTuberiasEditor> tiposTuberia;

	List<EnumTuberias.Tuberia> tuberiasCompatiblesSentido1;
	List<EnumTuberias.Tuberia> tuberiasCompatiblesSentido2;
	EnumTuberias.Tuberia tipoTuberia;
	EnumTuberias.Sentido sentidoHorizontal;
	EnumTuberias.Sentido sentidoVertical;

	bool puedeMoverse = true;
	bool llenoAgua = false;
	
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
				tuberiasCompatiblesSentido1 = tiposTuberia[i].tuberiasCompatiblesSentido1;
				tuberiasCompatiblesSentido2 = tiposTuberia[i].tuberiasCompatiblesSentido2;
				sentidoHorizontal = tiposTuberia[i].sentidoHorizontal;
				sentidoVertical = tiposTuberia[i].sentidoVertical;
				return;
			}
		}
	}	
	
	/**
	 * Activa la tuberia y despues de un tiempo comprueba la siguiente direccion a tomar
	**/
	public IEnumerator ActivarTuberia(GameObject [,]matrizSlots, int columnaActual, int filaActual, int desplazamientoHorizontal, int desplazamientoVertical, EnumTuberias.Tuberia orientacion){
		GetComponent<Image> ().color = new Color32(100,0,0,255);
		llenoAgua = true;
		puedeMoverse = false;
		yield return new WaitForSeconds(delayFlujo);
		GetComponent<Image> ().color = new Color32(0,0,100,255);
	
		int x = 0;
		int y = 0;
		bool final = false;
		bool cambiado = false;
		
		//orientacion del flujo horizontal
		if(orientacion == EnumTuberias.Tuberia.horizontal){	
			if(sentidoHorizontal == EnumTuberias.Sentido.derecha){
				desplazamientoHorizontal = 1;
				desplazamientoVertical = 0;
			}else if(sentidoHorizontal == EnumTuberias.Sentido.izquierda){
				desplazamientoHorizontal = -1;
				desplazamientoVertical = 0;
			}else if(sentidoHorizontal != EnumTuberias.Sentido.bidireccional){
				Debug.Log(sentidoHorizontal+" "+EnumTuberias.Sentido.derecha);
				Debug.Log("FIN DEL JUEGO HAS PERDIDO H");
				final = true;
			}//sentido bidireccional no cambia nada
			//no coincide los sentidos
			
			if(matrizSlots[columnaActual+desplazamientoHorizontal,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoHorizontal == EnumTuberias.Sentido.izquierda){
				x = -1;
			}else if(matrizSlots[columnaActual+desplazamientoHorizontal,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoHorizontal != EnumTuberias.Sentido.bidireccional){//distinto de bidireccional
				x = 1;
			}
			
			if(matrizSlots[columnaActual+desplazamientoHorizontal+x,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().llenoAgua == true){
				orientacion = EnumTuberias.Tuberia.vertical;
				cambiado = true;
			}
		}else if(orientacion == EnumTuberias.Tuberia.vertical){//orientacion del flujo vertical
			if(sentidoVertical == EnumTuberias.Sentido.arriba){
				desplazamientoVertical = -1;
				desplazamientoHorizontal = 0;
			}else if(sentidoVertical == EnumTuberias.Sentido.abajo){
				desplazamientoVertical = 1;
				desplazamientoHorizontal = 0;
			}if(sentidoVertical != EnumTuberias.Sentido.bidireccional){
				Debug.Log(sentidoHorizontal+" "+EnumTuberias.Sentido.derecha);
				Debug.Log("FIN DEL JUEGO HAS PERDIDO V");
				final = true;
			}//sentido bidireccional no cambia nada
			//no coincide los sentidos
			
			if(matrizSlots[columnaActual,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoVertical == EnumTuberias.Sentido.abajo){
				y = 1;
			}else if(matrizSlots[columnaActual,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoVertical != EnumTuberias.Sentido.bidireccional){// distinto de bidireccional
				y = -1;
			}
			
			if(columnaActual > 0 && matrizSlots[columnaActual,filaActual+y+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().llenoAgua == true){
				orientacion = EnumTuberias.Tuberia.horizontal;
				
			}
		}
	
		if(final == false)
			StartCoroutine(ActivarTuberia(matrizSlots, columnaActual+desplazamientoHorizontal, filaActual+desplazamientoVertical, desplazamientoHorizontal, desplazamientoVertical, orientacion));
	}
	
	/**
	 * Establece el valor si esta lleno de agua 
	**/
	public void SetpuedeMoverse(bool valor){
		puedeMoverse = valor;
	}
	
	/**
	 * Obtiene el valor si esta lleno de agua 
	**/
	public bool GetpuedeMoverse(){
		return puedeMoverse;
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
