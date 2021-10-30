using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TipoTuberia : MonoBehaviour
{
		
	//Tipos de tuberias
	[System.Serializable] public struct TiposTuberiasEditor{
		public EnumTuberias.Tuberia tipoDeTuberia;
		public Sprite imagenTuberia;
		//El sentido principal con la logica de que el agua va de izquierda a derecha
		public EnumTuberias.Sentido sentidoHorizontal;
		//El sentido opuesto cuando el agua cambia de direcion y se tiene que hacer una lectirura de derecha a izquierda
		public EnumTuberias.Sentido sentidoVertical;
	}
	public List<TiposTuberiasEditor> tiposTuberia;

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
				sentidoHorizontal = tiposTuberia[i].sentidoHorizontal;
				sentidoVertical = tiposTuberia[i].sentidoVertical;
				return;
			}
		}
	}	
	
	/**
	 * Activa la tuberia y despues de un tiempo comprueba la siguiente direccion a tomar
	 * @note El algortimo funciona de forma que obtiene la direccion horizontal y vertical de la tuberia, comprueba la orientacion del fluido, y comprueba la orientacion de la tuberia actual por donde saldrá dicho fluido
	 * dependiendo del sentido de la tuberia se establecera que el fluido fluya por la misma direccion que la tuberia
	 * despues obtiene el sentido de la siguiente tuberia a la que se dirije el fluido(en este punto implica que la tuberia tiene algun sentido de direccion) si tiene una orientacion hacia el contrario de donde se dirige el fluido se cambia la orientacion de este a vertical.
	 * en el caso de las tuberias bidireccionales no se realiza ninguna comprobacion simplemente se deja los indices como estan.
	**/
	public IEnumerator ActivarTuberia(GameObject [,]matrizSlots, int columnaActual, int filaActual, int desplazamientoHorizontal, int desplazamientoVertical, EnumTuberias.Tuberia orientacion){
		GetComponent<Image> ().color = new Color32(100,0,0,255);
		llenoAgua = true;
		puedeMoverse = false;
		
		bool final = false;

	
		
		//Dirige el flujo del fluido
		

	
		int x = 0;
		int y = 0;

		//orientacion del flujo horizontal
		if(orientacion == EnumTuberias.Tuberia.horizontal){	
			if(sentidoHorizontal == EnumTuberias.Sentido.derecha){
				desplazamientoHorizontal = 1;
				desplazamientoVertical = 0;
			}else if(sentidoHorizontal == EnumTuberias.Sentido.izquierda){
				desplazamientoHorizontal = -1;
				desplazamientoVertical = 0;
			}else if(sentidoHorizontal != EnumTuberias.Sentido.bidireccional){
				Debug.Log(sentidoHorizontal);
				Debug.Log("FIN DEL JUEGO HAS PERDIDO H");
				final = true;
			}//sentido bidireccional no cambia nada
			//no coincide los sentidos
			
				yield return new WaitForSeconds(delayFlujo);
			
			if(matrizSlots[columnaActual+desplazamientoHorizontal,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoHorizontal == EnumTuberias.Sentido.izquierda){
				x = -1;
			}else if(matrizSlots[columnaActual+desplazamientoHorizontal,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoHorizontal != EnumTuberias.Sentido.bidireccional){//distinto de bidireccional
				x = 1;
			}
			
			if(matrizSlots[columnaActual+desplazamientoHorizontal+x,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().llenoAgua == true){
				orientacion = EnumTuberias.Tuberia.vertical;
			}
			
		}else if(orientacion == EnumTuberias.Tuberia.vertical){
			//orientacion del flujo vertical
			if(sentidoVertical == EnumTuberias.Sentido.arriba){
				desplazamientoVertical = -1;
				desplazamientoHorizontal = 0;
			}else if(sentidoVertical == EnumTuberias.Sentido.abajo){
				desplazamientoVertical = 1;
				desplazamientoHorizontal = 0;
			}else if(sentidoVertical != EnumTuberias.Sentido.bidireccional){
				Debug.Log(sentidoVertical);
				Debug.Log("FIN DEL JUEGO HAS PERDIDO V");
				final = true;
			}//sentido bidireccional no cambia nada
			//no coincide los sentidos
			
				yield return new WaitForSeconds(delayFlujo);
			
			if(matrizSlots[columnaActual,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoVertical == EnumTuberias.Sentido.abajo){
				y = 1;
			}else if(matrizSlots[columnaActual,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoVertical != EnumTuberias.Sentido.bidireccional){// distinto de bidireccional
				y = -1;
			}
			
			if(matrizSlots[columnaActual,filaActual+y+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().llenoAgua == true){
				orientacion = EnumTuberias.Tuberia.horizontal;
				
			}
		}

		GetComponent<Image> ().color = new Color32(0,0,100,255);

		if(final == false)
			StartCoroutine(matrizSlots[columnaActual+desplazamientoHorizontal,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().ActivarTuberia(matrizSlots, columnaActual+desplazamientoHorizontal, filaActual+desplazamientoVertical, desplazamientoHorizontal, desplazamientoVertical, orientacion));
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
