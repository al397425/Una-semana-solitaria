using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearPuzzle : MonoBehaviour
{
	
	//Huecos de las tuberias
	public GameObject SlotTuberia;
	//Tuberias
	public GameObject Tuberia;
	//Referencia al canvas
	public Canvas canvas;
	
	//Matriz de los huecos donde iran las tuberias y por consecuente esta es la matriz de las tuberias
	protected GameObject [,]matrizSlots;
	
    // Start is called before the first frame update
    void Start()
    {
		iniciarMinijuego(15, 13, 13, 1, 30.0f);
        
    }
	
	public void iniciarMinijuego(int ancho, int alto, int filaPuntoInicio, int filaPuntoFinal, float delayFlujoTuberia){
		GenerarSlots(ancho,alto, filaPuntoInicio, filaPuntoFinal, delayFlujoTuberia);
	}
	
	
	/**
	 * Genera los slots del tablero ademas de las tuberias
	**/
	void GenerarSlots(int ancho, int alto, int filaPuntoInicio, int filaPuntoFinal, float delayFlujoTuberia){
		matrizSlots = new GameObject [ancho+2,alto];
		
		float posx=0,posy=0;
		
		Image imagen = SlotTuberia.GetComponent<Image> ();
		float anchoImagen=imagen.sprite.rect.width;
		float altoImagen=imagen.sprite.rect.height;
		
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
		tuberia.GetComponent<TipoTuberia>().SetpuedeArrastrar(false);
		tuberia.GetComponent<TipoTuberia>().SetdelayFlujo(delayFlujoTuberia);
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
			   //Pasa a la siguiente posicion
			   posx += anchoImagen;
           }
		   posy += altoImagen;
		   posx = anchoImagen;
       }
	   
	    //Crea el punto final
		//Crea slots
		posy = altoImagen;
		posx = anchoImagen;
		matrizSlots[alto+1, filaPuntoFinal-1] = Instantiate(SlotTuberia, new Vector2(0,0), Quaternion.identity);
		matrizSlots[alto+1, filaPuntoFinal-1].transform.SetParent(canvas.transform);
		matrizSlots[alto+1, filaPuntoFinal-1].GetComponent<RectTransform>().anchoredPosition = new Vector2(posx*(ancho+1), -posy*filaPuntoFinal);//multiplica la posicion del vector en x por las coordenadas en y
		//Crea tuberia
		tuberia = (GameObject)Instantiate(Tuberia, new Vector2(0,0), Quaternion.identity);
		tuberia.transform.SetParent(matrizSlots[alto+1, filaPuntoFinal-1].transform);
		tuberia.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		tuberia.GetComponent<TipoTuberia>().EstablecerTipoTuberia(EnumTuberias.Tuberia.fin);
		tuberia.GetComponent<TipoTuberia>().SetpuedeArrastrar(false);
		tuberia.GetComponent<TipoTuberia>().SetdelayFlujo(delayFlujoTuberia);
		posy = altoImagen;
		posx = anchoImagen;
		
		//Activa la tuberia inicial
		//StartCoroutine(matrizSlots[0,filaPuntoInicio-1].transform.GetChild(0).GetComponent<TipoTuberia>().ActivarTuberia(matrizSlots, 1, filaPuntoInicio-1, 1, 0));
	}

}