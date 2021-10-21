using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class SistemaDialogos : MonoBehaviour
{
	public enum Idioma {Espaniol, Ingles, Valenciano};
	
	//Personaje con el que se dialogara
	public int personaje = 0;
	//Idiomas
	public Idioma idiomaActual = Idioma.Espaniol;
	//CSV de los dialogos
	public TextAsset ficheroDialogos;
	//Texto de la ui que se modificara
	public TextMesh textoUI;
	
	List<string> dialogosNPC = new List<string>();
	List<string> dialogosPersonaje = new List<string>();
	
	//Obtiene la lista de dialogos usada durante la conversacion

    // Start is called before the first frame update
    void Start()
    {
		//string direccion = "C:\Users\al394516\Documents\GitHub\Una-semana-solitaria\Assets\Dialogos\dialogos prueba.csv";

		ObtenerListaDeDialogos();
		
		for(int i = 0; i< Math.Max(dialogosNPC.Count,dialogosPersonaje.Count) && (dialogosNPC[i]!="" || dialogosPersonaje[i] != ""); i++){
			if(dialogosNPC[i]!="")
				Debug.Log("Conchi: " + dialogosNPC[i]);
			if(dialogosPersonaje[i] != "")
				Debug.Log("F: " + dialogosPersonaje[i]);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void ObtenerListaDeDialogos(){
		using(var reader = new StreamReader(@AssetDatabase.GetAssetPath(ficheroDialogos)))
		{
			var line = reader.ReadLine();
			while (!reader.EndOfStream)
			{
				line = reader.ReadLine();
				var values = line.Split(';');
				switch(idiomaActual){
					case Idioma.Espaniol:
						if((values[(personaje*6)] !="" || values[(personaje*6) + 3] != "")){
							dialogosNPC.Add(values[(personaje*6)]);
							dialogosPersonaje.Add(values[(personaje*6) + 3]);
						}
					break;
					
					case Idioma.Ingles:
						if(values[(personaje*6) + 1] !="" || values[(personaje*6) + 4] != ""){
							dialogosNPC.Add(values[(personaje*6) + 1]);
							dialogosPersonaje.Add(values[(personaje*6) + 4]);
						}
					break;
					
					case Idioma.Valenciano:
						if(values[(personaje*6) + 2] !="" || values[(personaje*6) + 5] != ""){
							dialogosNPC.Add(values[(personaje*6) + 2]);
							dialogosPersonaje.Add(values[(personaje*6) + 5]);
						}
					break;
				}
			}
		}
	}
}
