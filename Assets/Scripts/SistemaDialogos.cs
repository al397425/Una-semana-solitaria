using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Events;
using TMPro;

public class SistemaDialogos : MonoBehaviour
{
	public enum Idioma {Espaniol, Ingles, Valenciano};
	//Idiomas
	public Idioma idiomaActual = Idioma.Espaniol;
	//CSV de los dialogos
	public TextAsset ficheroDialogos;
	//Texto de la ui que se modificara
	public TextMeshProUGUI textoUI;
	//Tecla de interaccion
	public KeyCode teclaDeInteraccion;
	//Tiempo de animacion
	public float tiempoPorCaracter = 0.02f;
	//Referencia a los retratos
	public Image referenciaRetratoDialogoNPC;
	public Image referenciaRetratoDialogoJugador;

	int indiceDialogoNPC, indiceDialogoJugador;

	bool hablaNPC=true;
	bool comenzarConversacion = true;
	
	Sprite referenciaSpriteNPC;
	
	List<string> dialogosNPC = new List<string>();
	List<string> dialogosPersonaje = new List<string>();
	
	IEnumerator corrutinaActual;
	
	UnityEvent eventoTerminarDialogo;
	
	Color32 colorBlanco = new Color32(255,255,255,255);
	Color32 colorGris = new Color32(100,100,100,255);
	
	//Obtiene la lista de dialogos usada durante la conversacion
	public void ObtenerListaDeDialogos(int personaje, Sprite retratoNPC, UnityEvent eventoAlTerminarDialogo){
		comenzarConversacion = false;
		indiceDialogoNPC = 0;
		indiceDialogoJugador = 0;
		referenciaSpriteNPC= retratoNPC;
		
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
		
		eventoTerminarDialogo = eventoAlTerminarDialogo;
		
		//Lo llama una vez para iniciar el dialogo
		AvanzarDialogo();
	}
	
	//Avanza en el dialogo
	public void AvanzarDialogo(){
		if((indiceDialogoNPC < dialogosNPC.Count && dialogosNPC[indiceDialogoNPC]!="") || (indiceDialogoJugador < dialogosPersonaje.Count && dialogosPersonaje[indiceDialogoJugador] != "")){
			if(dialogosNPC[indiceDialogoNPC]==""){
				referenciaRetratoDialogoJugador.color = colorBlanco;
				referenciaRetratoDialogoNPC.color = colorGris;
				if(corrutinaActual != null)
					StopCoroutine(corrutinaActual);
				corrutinaActual = escribirAnimacion(dialogosPersonaje[indiceDialogoJugador]);
				StartCoroutine(corrutinaActual);
				indiceDialogoJugador++;
				indiceDialogoNPC++;
				hablaNPC = false;
			}else if(dialogosPersonaje[indiceDialogoJugador]==""){
				referenciaRetratoDialogoJugador.color = colorGris;
				referenciaRetratoDialogoNPC.color = colorBlanco;
				if(corrutinaActual != null)
					StopCoroutine(corrutinaActual);
				corrutinaActual = escribirAnimacion(dialogosNPC[indiceDialogoNPC]);
				StartCoroutine(corrutinaActual);
				indiceDialogoJugador++;
				indiceDialogoNPC++;
				hablaNPC = true;
			}else{
				if(hablaNPC){
					referenciaRetratoDialogoJugador.color = colorGris;
					referenciaRetratoDialogoNPC.color = colorBlanco;
					if(corrutinaActual != null)
						StopCoroutine(corrutinaActual);
					corrutinaActual = escribirAnimacion(dialogosNPC[indiceDialogoNPC]);
					StartCoroutine(corrutinaActual);
					hablaNPC = false;
				}else{
					referenciaRetratoDialogoJugador.color = colorBlanco;
					referenciaRetratoDialogoNPC.color = colorGris;
					if(corrutinaActual != null)
						StopCoroutine(corrutinaActual);
					corrutinaActual = escribirAnimacion(dialogosPersonaje[indiceDialogoJugador]);
					StartCoroutine(corrutinaActual);
					indiceDialogoJugador++;
					indiceDialogoNPC++; 
					hablaNPC = true;
				}
			}
		}else{
			//desactivar el sistema
			if(eventoTerminarDialogo != null){
				eventoTerminarDialogo.Invoke();
			}
			dialogosNPC.Clear();
			dialogosPersonaje.Clear();
			comenzarConversacion = true;
			gameObject.SetActive(false);
		}
	}
	
	public bool GetcomenzarConversacion(){
		return comenzarConversacion;
	}
	
	public void SetcomenzarConversacion(bool valor){
		comenzarConversacion = valor;
	}
	
	IEnumerator escribirAnimacion(string texto){
		string textoActual="";
		
		for(int i =0; i<texto.Length; i++){
			textoActual += texto[i];
			textoUI.text = textoActual;
			yield return new WaitForSeconds(tiempoPorCaracter);
		}
		
	}
}
