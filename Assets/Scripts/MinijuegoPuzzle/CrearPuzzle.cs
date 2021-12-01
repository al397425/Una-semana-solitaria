using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearPuzzle : MonoBehaviour
{
	
	public float espaciHorizontalEntreTuberias = 40;
	public float espaciVerticalEntreTuberias = 40;
	
	//Huecos de las tuberias
	public GameObject SlotTuberia;
	//Tuberias
	public GameObject Tuberia;
	//Pantalla victoria
	public GameObject pantallaVictoria;
	//Pantalla derrota
	public GameObject pantallaDerrota;
	//Referencia al canvas
	public Canvas canvas;
	
	//Delay antes de que empiece a fluir el agua
	public float DelayAntesEmpezarPuzzle = 3.0f;
	
	//Clips de audio
	public AudioClip sonidoCogerTuberia;
	public AudioClip sonidoSoltarTuberia;
	public AudioClip sonidoDestaparTuberia;

	AudioSource fuenteAudio;
	
	//Matriz de los huecos donde iran las tuberias y por consecuente esta es la matriz de las tuberias
	protected GameObject [,]matrizSlots;

	bool puzzleInteractuable = true;
		
    // Start is called before the first frame update
    void Awake()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }
	
	public void iniciarMinijuego(int ancho, int alto, int filaPuntoInicio, int filaPuntoFinal, float delayFlujoTuberia, GameObject refPuzzle, int numHuecos){
		StartCoroutine(GenerarSlots(ancho,alto, filaPuntoInicio, filaPuntoFinal, delayFlujoTuberia, refPuzzle, numHuecos));
	}
	
	
	/**
	 * Genera los slots del tablero ademas de las tuberias
	**/
	IEnumerator GenerarSlots(int ancho, int alto, int filaPuntoInicio, int filaPuntoFinal, float delayFlujoTuberia, GameObject refPuzzle, int numHuecos){
		matrizSlots = new GameObject [ancho+2,alto];
		
		float posx=0,posy=0;
		
		Image imagen = SlotTuberia.GetComponent<Image> ();
		float anchoImagen= espaciHorizontalEntreTuberias;
		float altoImagen=espaciVerticalEntreTuberias;
		
		posy = altoImagen;
		
		GameObject tuberia;
		
		//Crea el punto de incio
		//Crea slots
		matrizSlots[0,filaPuntoInicio-1] = Instantiate(SlotTuberia, new Vector2(0,0), Quaternion.identity);
		matrizSlots[0,filaPuntoInicio-1].transform.SetParent(canvas.transform);
		matrizSlots[0,filaPuntoInicio-1].GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, -posy*filaPuntoInicio);//multiplica la posicion del vector en x por las coordenadas en y
		//Crea tuberia
		tuberia = (GameObject)Instantiate(Tuberia, new Vector2(0,0), Quaternion.identity);
		tuberia.transform.SetParent(matrizSlots[0,filaPuntoInicio-1].transform);
		tuberia.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		tuberia.GetComponent<TipoTuberia>().EstablecerTipoTuberia(EnumTuberias.Tuberia.inicio);
		tuberia.GetComponent<TipoTuberia>().SetpuedeMoverse(false);
		tuberia.GetComponent<TipoTuberia>().SetdelayFlujo(delayFlujoTuberia);
			//Quita la losa a la tuberia de inicio
		tuberia.transform.GetChild(2).gameObject.active = false;
		
		tuberia.GetComponent<ArrastrarSoltarTuberia>().SetreferenciaPuzzle(gameObject);
		posy = altoImagen;
		posx = anchoImagen;
		

		//Se empiza por el 1 ya que en el 0 ira el punto de partida
		for (int y=0; y < alto; y++){
           for (int x=1; x <= ancho; x++){
			   //Crea slot
               matrizSlots[x,y] = Instantiate(SlotTuberia, new Vector2(0,0), Quaternion.identity);
			   matrizSlots[x,y].transform.SetParent(canvas.transform);
			   matrizSlots[x,y].GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, -posy);
			   //Crea tuberia
			   tuberia = Instantiate(Tuberia, new Vector2(0,0), Quaternion.identity);
			   tuberia.transform.SetParent(matrizSlots[x,y].transform);
			   tuberia.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
			   tuberia.GetComponent<TipoTuberia>().SetdelayFlujo(delayFlujoTuberia);
			   tuberia.GetComponent<ArrastrarSoltarTuberia>().SetreferenciaPuzzle(gameObject);
			   //Pasa a la siguiente posicion
			   posx += anchoImagen;
           }
		   posy += altoImagen;
		   posx = anchoImagen;
       }

	   //Establece aleatoriamente los huecos en el tablero
	   for(int i = 0; i < numHuecos; i++){
		   //Genera un numero aleatorio de la fila y columna dentro del rango del tablero y que no tape la entrada y salida del flujo
			int x = Random.Range(2, ancho-1);
			int y = Random.Range(0, alto);
		   //Obtiene el hijo del slot que es la tuberia y llama a la funcion establecerTipo y pone tipo hueco
		   matrizSlots[x,y].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().EstablecerTipoTuberia(EnumTuberias.Tuberia.hueco);
		   matrizSlots[x,y].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().SetpuedeMoverse(false);
		   Debug.Log( matrizSlots[x,y].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().GetpuedeMoverse());
	   }
	   
		//Genera una solucion aleatoria
		List<EnumTuberias.Tuberia> caminoGenerado = generarSolucion(ancho, alto, filaPuntoInicio, filaPuntoFinal);

		//Cambia el tipo de tuberias aleatoriamente si no es un hueco
		for(int i = 0; i < caminoGenerado.Count; ){
			//Genera un numero aleatorio de la fila y columna dentro del rango del tablero y que no tape la entrada y salida del flujo
			int x = Random.Range(1, ancho);
			int y = Random.Range(0, alto);

			if(matrizSlots[x,y].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().GettipoTuberia() != EnumTuberias.Tuberia.hueco){
				 matrizSlots[x,y].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().EstablecerTipoTuberia(caminoGenerado[i]);
				 i++;
			}
		}

	    //Crea el punto final
		//Crea slots
		posy = altoImagen;
		posx = anchoImagen;
		matrizSlots[ancho+1, filaPuntoFinal-1] = Instantiate(SlotTuberia, new Vector2(0,0), Quaternion.identity);
		matrizSlots[ancho+1, filaPuntoFinal-1].transform.SetParent(canvas.transform);
		matrizSlots[ancho+1, filaPuntoFinal-1].GetComponent<RectTransform>().anchoredPosition = new Vector2(posx*(ancho+1), -posy*filaPuntoFinal);//multiplica la posicion del vector en x por las coordenadas en y
		//Crea tuberia
		tuberia = (GameObject)Instantiate(Tuberia, new Vector2(0,0), Quaternion.identity);
		tuberia.transform.SetParent(matrizSlots[ancho+1, filaPuntoFinal-1].transform);
		tuberia.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		tuberia.GetComponent<TipoTuberia>().EstablecerTipoTuberia(EnumTuberias.Tuberia.fin);
		tuberia.GetComponent<TipoTuberia>().SetpuedeMoverse(false);
		tuberia.GetComponent<TipoTuberia>().SetdelayFlujo(delayFlujoTuberia);
		tuberia.GetComponent<ArrastrarSoltarTuberia>().SetreferenciaPuzzle(gameObject);
			//Quita la losa a la tuberia de inicio
		tuberia.transform.GetChild(2).gameObject.active = false;
		
		posy = altoImagen;
		posx = anchoImagen;
		
		//Activa la tuberia inicial
		yield return new WaitForSeconds(DelayAntesEmpezarPuzzle);
		StartCoroutine(matrizSlots[0,filaPuntoInicio-1].transform.GetChild(0).GetComponent<TipoTuberia>().ActivarTuberia(matrizSlots, 0, filaPuntoInicio-1, 0, 0, EnumTuberias.Tuberia.horizontal, refPuzzle, gameObject));
		yield break;
	}

	List<EnumTuberias.Tuberia> generarSolucion(int ancho, int alto, int filaPuntoInicio, int filaPuntoFinal){

		EnumTuberias.Tuberia orientacionTuberia;
		List<EnumTuberias.Tuberia> camino = new List<EnumTuberias.Tuberia>();

		//Comprueba si el objetivo esta por encima o abajo o a la misma altura del inicio para saber si tiene que construir el camino subiendo o bajando
		int alturaVertical = 0;

			//Si esta por debajo del final, es decir el camino tiene que subir
		if(filaPuntoInicio > filaPuntoFinal){
			alturaVertical = -1;
		}else if(filaPuntoInicio < filaPuntoFinal){
			alturaVertical = 1;
		}
		
		int diferenciaAltura = Mathf.Abs(filaPuntoInicio - filaPuntoFinal);
		
		//Crea el camino horizontal hasta el final exceptuando uno que se encarga de indicar si crea otra orizontal o una que suba u otra que baje
		for(int x = 1; x < ancho; x++){
			if(matrizSlots[x+1,filaPuntoInicio-1].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().GettipoTuberia() != EnumTuberias.Tuberia.hueco){
				camino.Add(EnumTuberias.Tuberia.horizontal);
			}else{
				if(alturaVertical == -1){
					camino.Add(EnumTuberias.Tuberia.izquierdaArriba);
					//Si al subir tiene un hueco contiguo al de abajo
					int indiceSubida = 1;
					while(matrizSlots[x+1,filaPuntoInicio-1 - indiceSubida].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().GettipoTuberia() == EnumTuberias.Tuberia.hueco){
						camino.Add(EnumTuberias.Tuberia.vertical);
						diferenciaAltura--;
						indiceSubida++;
					}
					camino.Add(EnumTuberias.Tuberia.derechaAbajo);
					camino.Add(EnumTuberias.Tuberia.horizontal);
				}else{
					camino.Add(EnumTuberias.Tuberia.izquierdaAbajo);
					//Si al subir tiene un hueco contiguo al de abajo
					int indiceSubida = 1;
					while(matrizSlots[x+1,filaPuntoInicio-1 + indiceSubida].transform.GetChild(0).gameObject.GetComponent<TipoTuberia>().GettipoTuberia() == EnumTuberias.Tuberia.hueco){
						camino.Add(EnumTuberias.Tuberia.vertical);
						diferenciaAltura--;
						indiceSubida++;
					}
					camino.Add(EnumTuberias.Tuberia.derechaArriba);
					camino.Add(EnumTuberias.Tuberia.horizontal);
				}
			}
		}


			//Crea tuberia hacia arriba o abajo dependiendo de si la altura es positiva o negativa y añade las tuberas finales que conectara con el final
		if(alturaVertical == -1){
			camino.Add(EnumTuberias.Tuberia.izquierdaArriba);
			camino.Add(EnumTuberias.Tuberia.derechaAbajo);
		}else if(alturaVertical == 1){
			camino.Add(EnumTuberias.Tuberia.izquierdaAbajo);
			camino.Add(EnumTuberias.Tuberia.derechaArriba);
		}else{
			camino.Add(EnumTuberias.Tuberia.horizontal);
		}

		for(int i = 0; i < diferenciaAltura-1;){
			camino.Add(EnumTuberias.Tuberia.vertical);
			diferenciaAltura--;
		}
		

		return camino;
	}

	/**
	 * Retorna el componente AudioSource
	**/
	public AudioSource GetfuenteAudio(){
		return fuenteAudio;
	}

	/**
	 * Retorna el valor de puzzleInteractuable
	**/
	public bool GetpuzzleInteractuable(){
		return puzzleInteractuable;
	}

	/**
	 * Establece el valor de puzzleInteractuable
	**/
	public void SetpuzzleInteractuable(bool valor){
		puzzleInteractuable = valor;
	}
}