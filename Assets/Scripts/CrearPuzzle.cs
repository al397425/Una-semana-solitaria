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
		iniciarMinijuego(5);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void iniciarMinijuego(int tamanio){
		GenerarSlots(5);
		GenerarTuberias(5);
	}
	
	void GenerarSlots(int tamanio){
		matrizSlots = new GameObject [tamanio,tamanio];
		
		float posx=0,posy=0;
		
		Image imagen = SlotTuberia.GetComponent<Image> ();
		float anchoImagen=imagen.sprite.rect.width;
		float altoImagen=imagen.sprite.rect.height;
		
		for (int y=0; y < tamanio; y++)
       {
           for (int x=0; x < tamanio; x++)
           {
               matrizSlots[x,y] = Instantiate(SlotTuberia, new Vector3(posx,posy,0), Quaternion.identity);
			   matrizSlots[x,y].transform.SetParent(canvas.transform);
			   posx += anchoImagen;
           }
		   posy += altoImagen;
		   posx = 0;
       }  
	}
	
	void GenerarTuberias(int tamanio){
		
	}
}