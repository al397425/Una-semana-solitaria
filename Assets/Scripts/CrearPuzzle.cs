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
	GameObject [,]matrizSlots;
	GameObject [,]matrizTuberias;
	
    // Start is called before the first frame update
    void Start()
    {
		iniciarMinijuego(27,13);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void iniciarMinijuego(int ancho, int alto){
		GenerarSlots(ancho,alto);
		GenerarTuberias(ancho,alto);
	}
	
	void GenerarSlots(int ancho, int alto){
		matrizSlots = new GameObject [ancho,alto];
		
		float posx=0,posy=0;
		
		Image imagen = SlotTuberia.GetComponent<Image> ();
		float anchoImagen=imagen.sprite.rect.width;
		float altoImagen=imagen.sprite.rect.height;
		
		posy = altoImagen;
		
		for (int y=0; y < alto; y++)
       {
           for (int x=0; x < ancho; x++)
           {
               matrizSlots[x,y] = Instantiate(SlotTuberia, new Vector2(0,0), Quaternion.identity);
			   matrizSlots[x,y].transform.SetParent(canvas.transform);
			   matrizSlots[x,y].GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, -posy);
			   posx += anchoImagen;
           }
		   posy += altoImagen;
		   posx = 0;
       }  
	}
	
	void GenerarTuberias(int ancho, int alto){
		
	}
}