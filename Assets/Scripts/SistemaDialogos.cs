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
	//public TextAsset ficheroDialogos;
	//Texto de la ui que se modificara
	public TextMeshProUGUI textoUI;
	//Tecla de interaccion
	public KeyCode teclaDeInteraccion;
	//Tiempo de animacion
	public float tiempoPorCaracter = 0.02f;
	//Referencia a los retratos
	public Image referenciaRetratoDialogoNPC;
	public Image referenciaRetratoDialogoJugador;
	//
	public AudioSource sonidoDelTexto;
	
	//CSV de los dialogos
	public string directorioArchivoDialogos = "/Dialogos/dialogos prueba.csv";

	int indiceDialogoNPC, indiceDialogoJugador;
	
	//Tecla de interaccion
	public KeyCode teclaAvanzarDialogo = KeyCode.Space;

	bool hablaNPC=true;
	bool comenzarConversacion = true;
	
	Sprite referenciaSpriteNPC;
	
	List<string> dialogosNPC = new List<string>();
	List<string> dialogosPersonaje = new List<string>();
	
	IEnumerator corrutinaActual;
	
	UnityEvent eventoTerminarDialogo;
	
	bool reactivaConversacion = true;
	

	
	Color32 colorBlanco = new Color32(255,255,255,255);
	Color32 colorGris = new Color32(100,100,100,255);
	
	
	void FixedUpdate(){
		if(Input.GetKeyDown(teclaAvanzarDialogo) && comenzarConversacion == false){
			AvanzarDialogo();
		}
	}
	
	public bool GetReactivarConversacion(){
		return reactivaConversacion;
	}
	
	IEnumerator reactivarConversacion(){
		
		 yield return new WaitForSeconds(0.1f);
		 gameObject.SetActive(false);
		reactivaConversacion = true;
	}
	
	//Obtiene la lista de dialogos usada durante la conversacion y hace las asignaciones de los parametro del NPC
	public void ObtenerListaDeDialogos(int personaje, Sprite retratoNPC, UnityEvent eventoAlTerminarDialogo){
		reactivaConversacion = false;
		comenzarConversacion = false;
		indiceDialogoNPC = 0;
		indiceDialogoJugador = 0;
		referenciaSpriteNPC= retratoNPC;
		eventoTerminarDialogo = eventoAlTerminarDialogo;
			Debug.Log(Application.dataPath);
		using(var reader = new StreamReader(@Application.dataPath+directorioArchivoDialogos))
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
			
			StartCoroutine(reactivarConversacion());
		}
	}
	
	public bool GetcomenzarConversacion(){
		return comenzarConversacion;
	}
	
	public void SetcomenzarConversacion(bool valor){
		comenzarConversacion = valor;
	}
	
	//Escribe un caracter hasta completar el texto dado
	IEnumerator escribirAnimacion(string texto){
		string textoActual="";
		
		for(int i =0; i<texto.Length; i++){
			textoActual += texto[i];
			textoUI.text = textoActual;
			if(sonidoDelTexto != null && !sonidoDelTexto.isPlaying)
				sonidoDelTexto.Play(0);
			yield return new WaitForSeconds(tiempoPorCaracter);
		}
		sonidoDelTexto.Stop();
	}
}
