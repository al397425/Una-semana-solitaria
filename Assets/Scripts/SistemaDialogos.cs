using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;

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
	public TextMeshProUGUI textoUI;
	//Boton para pasar dialogo

	int indiceDialogoNPC, indiceDialogoJugador;

	bool hablaNPC=true;
	
	List<string> dialogosNPC = new List<string>();
	List<string> dialogosPersonaje = new List<string>();
	
    // Start is called before the first frame update
    void Start()
    {	
ObtenerListaDeDialogos();
AvanzarDialogo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	//Obtiene la lista de dialogos usada durante la conversacion
	void ObtenerListaDeDialogos(){
		indiceDialogoNPC = 0;
		indiceDialogoJugador = 0;
		
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
	
	//Avanza en el dialogo
	public void AvanzarDialogo(){
		if((indiceDialogoNPC < dialogosNPC.Count && dialogosNPC[indiceDialogoNPC]!="") || (indiceDialogoJugador < dialogosPersonaje.Count && dialogosPersonaje[indiceDialogoJugador] != "")){
			if(dialogosNPC[indiceDialogoNPC]==""){
				textoUI.text = dialogosPersonaje[indiceDialogoJugador];
				indiceDialogoJugador++;
				indiceDialogoNPC++;
				hablaNPC = false;
			}else if(dialogosPersonaje[indiceDialogoJugador]==""){
				textoUI.text = dialogosNPC[indiceDialogoNPC];
				indiceDialogoJugador++;
				indiceDialogoNPC++;
				hablaNPC = true;
			}else{
				if(hablaNPC){
					textoUI.text = dialogosNPC[indiceDialogoNPC];
					hablaNPC = false;
				}else{
					textoUI.text = dialogosPersonaje[indiceDialogoJugador];
					indiceDialogoJugador++;
					indiceDialogoNPC++; 
					hablaNPC = true;
				}
			}
		}
		//desactivar HUD
		//this.SetActive(true);
	}
}
