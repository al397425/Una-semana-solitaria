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
	GameObject [,]matrizSlots;
	
    // Start is called before the first frame update
    void Start()
    {
		iniciarMinijuego(15,13, 7,1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void iniciarMinijuego(int ancho, int alto, int filaPuntoInicio, int filaPuntoFinal){
		GenerarSlots(ancho,alto, filaPuntoInicio, filaPuntoFinal);
	}
	
	
	/**
	 * Genera los slots del tablero ademas de las tuberias
	**/
	void GenerarSlots(int ancho, int alto, int filaPuntoInicio, int filaPuntoFinal){
		matrizSlots = new GameObject [ancho+2,alto+2];
		
		float posx=0,posy=0;
		
		Image imagen = SlotTuberia.GetComponent<Image> ();
		float anchoImagen=imagen.sprite.rect.width;
		float altoImagen=imagen.sprite.rect.height;
		
		posy = altoImagen;
		
		GameObject tuberia;
		
		//Crea el punto de incio
		//Crea slots
		matrizSlots[filaPuntoInicio,0] = Instantiate(SlotTuberia, new Vector2(0,0), Quaternion.identity);
		matrizSlots[filaPuntoInicio,0].transform.SetParent(canvas.transform);
		matrizSlots[filaPuntoInicio,0].GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, -posy*filaPuntoInicio);//multiplica la posicion del vector en x por las coordenadas en y
		//Crea tuberia
		tuberia = (GameObject)Instantiate(Tuberia, new Vector2(0,0), Quaternion.identity);
		tuberia.transform.SetParent(matrizSlots[filaPuntoInicio,0].transform);
		tuberia.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		tuberia.GetComponent<TipoTuberia>().EstablecerTipoTuberia(EnumDireccion.Direccion.ninguno, EnumDireccion.Direccion.derecha);
		posy = altoImagen;
		posx = anchoImagen;
		
		//Se empiza por el 1 ya que en el 0 ira el punto de partida
		for (int y=1; y <= alto; y++){
           for (int x=1; x <= ancho; x++){
			   //Crea slot
               matrizSlots[x,y] = Instantiate(SlotTuberia, new Vector2(0,0), Quaternion.identity);
			   matrizSlots[x,y].transform.SetParent(canvas.transform);
			   matrizSlots[x,y].GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, -posy);
			   //Crea tuberia
			   tuberia = Instantiate(Tuberia, new Vector2(0,0), Quaternion.identity);
			   tuberia.transform.SetParent(matrizSlots[x,y].transform);
			   tuberia.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
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
		matrizSlots[filaPuntoFinal, alto+1] = Instantiate(SlotTuberia, new Vector2(0,0), Quaternion.identity);
		matrizSlots[filaPuntoFinal, alto+1].transform.SetParent(canvas.transform);
		matrizSlots[filaPuntoFinal, alto+1].GetComponent<RectTransform>().anchoredPosition = new Vector2(posx*(ancho+1), -posy*filaPuntoFinal);//multiplica la posicion del vector en x por las coordenadas en y
		//Crea tuberia
		tuberia = (GameObject)Instantiate(Tuberia, new Vector2(0,0), Quaternion.identity);
		tuberia.transform.SetParent(matrizSlots[filaPuntoFinal, alto+1].transform);
		tuberia.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		tuberia.GetComponent<TipoTuberia>().EstablecerTipoTuberia(EnumDireccion.Direccion.izquierda, EnumDireccion.Direccion.ninguno);
		posy = altoImagen;
		posx = anchoImagen;
	}

}