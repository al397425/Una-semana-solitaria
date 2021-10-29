using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TipoTuberia : MonoBehaviour
{
		
	//Direcciones de entrada y salida de cada tuberia
	[System.Serializable] public struct Direcciones {
		public EnumDireccion.Direccion direccion1;
		public EnumDireccion.Direccion direccion2;
		
		//Constructor
		public Direcciones(EnumDireccion.Direccion d1, EnumDireccion.Direccion d2){
			direccion1 = d1;
			direccion2 = d2;
		}
		//Constructor vacio implicito

	}

	//Tipos de tuberias
	[System.Serializable] public struct TiposTuberiasEditor{
		public Direcciones tiposDireccionTuberia;
		public Sprite imagenTuberia;
	}
	public List<TiposTuberiasEditor> tiposTuberia;
	//-----------------------------------------------------------------------------------------------
	
    // Start is called before the first frame update
    void Awake()
    {
        //!!!!!!!! PROVISIONAL !!!!!!!!!!!!!!!!!!!
			int indice = Random.Range(0, 6);
			EstablecerTipoTuberia(tiposTuberia[indice].tiposDireccionTuberia.direccion1, tiposTuberia[indice].tiposDireccionTuberia.direccion2);
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
	public void EstablecerTipoTuberia(EnumDireccion.Direccion d1, EnumDireccion.Direccion d2){
		for(int i = 0; i < tiposTuberia.Count; i++){
			if(d1 == tiposTuberia[i].tiposDireccionTuberia.direccion1 && d2 == tiposTuberia[i].tiposDireccionTuberia.direccion2){
				GetComponent<Image> ().sprite = tiposTuberia[i].imagenTuberia;
				return;
			}
		}
	}	
}
