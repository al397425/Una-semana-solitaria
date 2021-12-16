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
		public Sprite imagenFluidoTuberia;
		//El sentido principal con la logica de que el agua va de izquierda a derecha
		public EnumTuberias.Sentido sentidoHorizontal;
		//El sentido opuesto cuando el agua cambia de direcion y se tiene que hacer una lectirura de derecha a izquierda
		public EnumTuberias.Sentido sentidoVertical;
		//Direcciones desde donde se rellena el fluido visualmente(la imagen)
		public Image.FillMethod metodoRellenoImagenFluido;
	}
	public List<TiposTuberiasEditor> tiposTuberia;

	EnumTuberias.Tuberia tipoTuberia;
	EnumTuberias.Sentido sentidoHorizontal;
	EnumTuberias.Sentido sentidoVertical;

	Image.FillMethod metodoRellenoImagenFluido;

	bool puedeMoverse = false;
	bool llenoAgua = false;
	bool empezarAnimacion = false;
	
	float delayFlujo =5.0f;
	


    // Start is called before the first frame update
    void Awake()
    {
        //Genera la tuberia
			int indice = Random.Range(0, 6);
			EstablecerTipoTuberia(tiposTuberia[indice].tipoDeTuberia);
    }

    // Update is called once per frame
    void Update()
    {
        //Ejecuta la animacion de llenar la tuberia
		if(empezarAnimacion == true && gameObject.transform.GetChild(1).GetComponent<Image> ().fillAmount < 1){
			gameObject.transform.GetChild(0).GetComponent<Image> ().fillAmount += (float)1.0/delayFlujo * Time.deltaTime;
		}
    }
	
	/** Establece el sprite de la tuberia y la direccion que tendra
	* @note No se usa diccionarios para una busqueda mas rapida ya que unity no deja exponer diccionarios
	* Por lo tanto habria que exponer una lista de estructuras y pasar eso al diccionario con lo que tendria un costo O(N)
	* De esta forma tambien tiene costo O(N)
	**/
	public void EstablecerTipoTuberia(EnumTuberias.Tuberia tuberia){
		for(int i = 0; i < tiposTuberia.Count; i++){
			if(tuberia == tiposTuberia[i].tipoDeTuberia){
				gameObject.transform.GetChild(1).GetComponent<Image> ().sprite = tiposTuberia[i].imagenTuberia;
				gameObject.transform.GetChild(0).GetComponent<Image> ().sprite = tiposTuberia[i].imagenFluidoTuberia;
				tipoTuberia = tiposTuberia[i].tipoDeTuberia;
				//Establece el metodo de relleno de la imagen del fluido
				metodoRellenoImagenFluido = tiposTuberia[i].metodoRellenoImagenFluido;
				gameObject.transform.GetChild(0).GetComponent<Image> ().fillMethod = metodoRellenoImagenFluido;
				//Pone la orientacion de las tuberias curvas porque su sentido lo marcara si es horario o antihorario, no como las horizontales y verticales que solo deja que lo marque el metodo de rellenado
				if(metodoRellenoImagenFluido == Image.FillMethod.Radial90){
					switch(tipoTuberia){
						case EnumTuberias.Tuberia.izquierdaArriba:
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin = (int)Image.Origin90.TopLeft;
						break;

						case EnumTuberias.Tuberia.izquierdaAbajo:
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin = (int)Image.Origin90.BottomLeft;
						break;

						case EnumTuberias.Tuberia.derechaArriba:
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin = (int)Image.Origin90.TopRight;
						break;

						case EnumTuberias.Tuberia.derechaAbajo:
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin = (int)Image.Origin90.BottomRight;
						break;
					}
				}

				//Establece la cantidad rellenada de la imagen a 0
				gameObject.transform.GetChild(1).GetComponent<Image> ().fillAmount = 0;

				sentidoHorizontal = tiposTuberia[i].sentidoHorizontal;
				sentidoVertical = tiposTuberia[i].sentidoVertical;
				return;
			}
		}
	}	
	
	/**
	 * Activa la tuberia y despues de un tiempo comprueba la siguiente direccion a tomar
	 * @note El algortimo funciona de forma que obtiene la direccion horizontal y vertical de la tuberia, comprueba la orientacion del fluido, y comprueba la orientacion de la tuberia actual por donde saldr� dicho fluido
	 * dependiendo del sentido de la tuberia se establecera que el fluido fluya por la misma direccion que la tuberia
	 * despues obtiene el sentido de la siguiente tuberia a la que se dirije el fluido(en este punto implica que la tuberia tiene algun sentido de direccion) si tiene una orientacion hacia el contrario de donde se dirige el fluido se cambia la orientacion de este a vertical.
	 * en el caso de las tuberias bidireccionales no se realiza ninguna comprobacion simplemente se deja los indices como estan.
	**/
	public IEnumerator ActivarTuberia(GameObject [,]matrizSlots, int columnaActual, int filaActual, int desplazamientoHorizontal, int desplazamientoVertical, EnumTuberias.Tuberia orientacion, GameObject refPuzzle, GameObject tablero){
		bool final = false;
		if(tipoTuberia == EnumTuberias.Tuberia.fin){
			Debug.Log("Ganastes");
			final = true;
			refPuzzle.GetComponent<CrearPuzzleActivador>().Setresuelto(true);
			refPuzzle.GetComponent<CrearPuzzleActivador>().eventoAlGanarElMinijuego.Invoke();
			if(refPuzzle.GetComponent<CrearPuzzleActivador>().puzzleTuberia.GetComponent<CrearPuzzle>().pantallaVictoria != null){
				GameObject pantallaVictoria = Instantiate(refPuzzle.GetComponent<CrearPuzzleActivador>().puzzleTuberia.GetComponent<CrearPuzzle>().pantallaVictoria, new Vector2(0,0), Quaternion.identity);
				pantallaVictoria.GetComponent<ReferenciaPuzzle>().SetRefTablero(tablero);
			}
			Time.timeScale = 1.0f;
			yield break;
		}
		llenoAgua = true;
		puedeMoverse = false;
		
		//Si choca con una tuberia tapada o un hueco en el tablero
		if(gameObject.transform.GetChild(2).gameObject.active == true || tipoTuberia == EnumTuberias.Tuberia.hueco){
			mostrarPantallaDerrota(refPuzzle, tablero);
			final = true;
			yield break;
		}

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
			
					
			//Establece la orientacion de la imagen del flujo
			if(desplazamientoHorizontal == -1){
				switch(gameObject.transform.GetChild(0).GetComponent<Image> ().fillMethod){
					case Image.FillMethod.Horizontal:
						gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin = (int)Image.OriginHorizontal.Right;
					break;

					case Image.FillMethod.Radial90:
						if(tipoTuberia == EnumTuberias.Tuberia.izquierdaArriba){
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillClockwise = true;
						}else{
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillClockwise = false;
						}
					break;

				}
			}else {
				switch(gameObject.transform.GetChild(0).GetComponent<Image> ().fillMethod){
					case Image.FillMethod.Horizontal:
						gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin = (int)Image.OriginHorizontal.Left;
					break;

					case Image.FillMethod.Radial90:
						if(tipoTuberia == EnumTuberias.Tuberia.izquierdaArriba || tipoTuberia == EnumTuberias.Tuberia.izquierdaAbajo || tipoTuberia == EnumTuberias.Tuberia.derechaArriba){
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillClockwise = false;
						}else{
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillClockwise = true;
						}
					break;
				}
			}
			
			Debug.Log("Orientacion: "+orientacion+ "  Tipo: "+tipoTuberia+"  Origen: "+gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin +"  Desplazamiento: "+desplazamientoHorizontal);
			
			empezarAnimacion = true;

			yield return new WaitForSeconds(delayFlujo);

			//Comprueba el sentido de la tuberia que le toca es la correcta y haya continuidad (si el sentido de la tuberia actual es derecho la siguiente tiene que ser izquierda y viceversa)
            // teniendo en cuenta el desplazamiento realizado para distinguir entre un sentido del flujo hacia la izquierda o derecha ya que si no se comprueba siempre se metera en una de las dos condiciones
			//cuando sea una tuberia bidireccional
			//Tambien detecta si el flujo a salido del tablero,
			if(matrizSlots[columnaActual+desplazamientoHorizontal,filaActual] == null || (desplazamientoHorizontal == 1 && (sentidoHorizontal == EnumTuberias.Sentido.derecha || sentidoHorizontal == EnumTuberias.Sentido.bidireccional) && matrizSlots[columnaActual+desplazamientoHorizontal,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoHorizontal == EnumTuberias.Sentido.derecha) || (desplazamientoHorizontal == -1 && (sentidoHorizontal == EnumTuberias.Sentido.izquierda || sentidoHorizontal == EnumTuberias.Sentido.bidireccional) && matrizSlots[columnaActual+desplazamientoHorizontal,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoHorizontal == EnumTuberias.Sentido.izquierda) ||  matrizSlots[columnaActual+desplazamientoHorizontal,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoVertical == EnumTuberias.Sentido.bidireccional){
                mostrarPantallaDerrota(refPuzzle, tablero);
				final = true;
				yield break;
            }
			
			if(final == false){
				if(matrizSlots[columnaActual+desplazamientoHorizontal,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoHorizontal == EnumTuberias.Sentido.izquierda){
					x = -1;
				}else if(matrizSlots[columnaActual+desplazamientoHorizontal,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoHorizontal != EnumTuberias.Sentido.bidireccional){//distinto de bidireccional
					x = 1;
				}
				
				if(matrizSlots[columnaActual+desplazamientoHorizontal+x,filaActual].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().llenoAgua == true){
					orientacion = EnumTuberias.Tuberia.vertical;
				}
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
			
						//Establece la orientacion de la imagen del flujo
			if(desplazamientoVertical == -1){
				switch(gameObject.transform.GetChild(0).GetComponent<Image> ().fillMethod){
					case Image.FillMethod.Vertical:
						gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin = (int)Image.OriginVertical.Bottom;
					break;

					case Image.FillMethod.Radial90:
						if(tipoTuberia == EnumTuberias.Tuberia.izquierdaArriba){
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillClockwise = false;
						}else{
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillClockwise = true;
						}
					break;

				}
			}else {
				switch(gameObject.transform.GetChild(0).GetComponent<Image> ().fillMethod){
					case Image.FillMethod.Vertical:
						gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin = (int)Image.OriginVertical.Top;
					break;

					case Image.FillMethod.Radial90:
						if(tipoTuberia == EnumTuberias.Tuberia.izquierdaArriba || tipoTuberia == EnumTuberias.Tuberia.izquierdaAbajo){
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillClockwise = true;
						}else{
							gameObject.transform.GetChild(0).GetComponent<Image> ().fillClockwise = false;
						}
					break;

				}
			}
			
			Debug.Log("Orientacion: "+orientacion+ "  Tipo: "+tipoTuberia+"  Origen: "+gameObject.transform.GetChild(0).GetComponent<Image> ().fillOrigin +"  Desplazamiento: "+desplazamientoVertical);
			
			empezarAnimacion = true;
			
			yield return new WaitForSeconds(delayFlujo);

			//Comprueba el sentido de la tuberia que le toca es la correcta y haya continuidad (si el sentido de la tuberia actual es arriba la siguiente tiene que ser abajo y viceversa)
            // teniendo en cuenta el desplazamiento realizado para distinguir entre un sentido del flujo hacia arriba o abajo ya que si no se comprueba siempre se metera en una de las dos condiciones
			//cuando sea una tuberia bidireccional
			//Tambien detecta si el flujo a salido del tablero
			if(filaActual+desplazamientoVertical < 0 || filaActual+desplazamientoVertical >= matrizSlots.GetLength(1) || (desplazamientoVertical == -1 && (sentidoVertical == EnumTuberias.Sentido.arriba || sentidoVertical == EnumTuberias.Sentido.bidireccional) && matrizSlots[columnaActual,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoVertical == EnumTuberias.Sentido.arriba) || (desplazamientoVertical == 1 && (sentidoVertical == EnumTuberias.Sentido.abajo || sentidoVertical == EnumTuberias.Sentido.bidireccional) && matrizSlots[columnaActual,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoVertical == EnumTuberias.Sentido.abajo) || matrizSlots[columnaActual,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoHorizontal == EnumTuberias.Sentido.bidireccional){
               mostrarPantallaDerrota(refPuzzle, tablero);
			   final = true;
			   yield break;
            }

			if(final == false){
				if(matrizSlots[columnaActual,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoVertical == EnumTuberias.Sentido.abajo){
					y = 1;
				}else if(matrizSlots[columnaActual,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().sentidoVertical != EnumTuberias.Sentido.bidireccional){// distinto de bidireccional
					y = -1;
				}
				
				if(matrizSlots[columnaActual,filaActual+y+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().llenoAgua == true){
					orientacion = EnumTuberias.Tuberia.horizontal;
					
				}
			}
		}

		if(final == false){
			StartCoroutine(matrizSlots[columnaActual+desplazamientoHorizontal,filaActual+desplazamientoVertical].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().ActivarTuberia(matrizSlots, columnaActual+desplazamientoHorizontal, filaActual+desplazamientoVertical, desplazamientoHorizontal, desplazamientoVertical, orientacion, refPuzzle, tablero));
		}
	}

	void mostrarPantallaDerrota(GameObject refPuzzle, GameObject tablero){
 		Debug.Log("FIN DEL JUEGO HAS PERDIDO V");
		refPuzzle.GetComponent<CrearPuzzleActivador>().eventoAlPerderElMinijuego.Invoke();
		empezarAnimacion = false;
		gameObject.transform.GetChild(0).GetComponent<Image> ().fillAmount = 0;
		
		if(refPuzzle.GetComponent<CrearPuzzleActivador>().puzzleTuberia.GetComponent<CrearPuzzle>().pantallaDerrota != null){
			GameObject pantallaDerrota = Instantiate(refPuzzle.GetComponent<CrearPuzzleActivador>().puzzleTuberia.GetComponent<CrearPuzzle>().pantallaDerrota, new Vector2(0,0), Quaternion.identity);
			pantallaDerrota.GetComponent<ReferenciaPuzzle>().SetRefTablero(tablero);
			pantallaDerrota.GetComponent<ReferenciaPuzzle>().SetRefActivador(refPuzzle);
		}
		Time.timeScale = 1.0f;
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

	/**
	 * Obtiene el tipo de tuberia
	**/
	public EnumTuberias.Tuberia GettipoTuberia(){
		return tipoTuberia;
	}
}



