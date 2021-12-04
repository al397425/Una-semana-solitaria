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
	//Texto del nombre del npc que se modificara
	public TextMeshProUGUI textoNombreUI;

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
	public string nombreArchivoDialogos = "dialogos prueba.csv";

	public string nombreProtagonista = "F.García";

	string nombreNpc;

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
	
	Animator anim;

	GameObject jugador;

	
	Color32 colorBlanco = new Color32(255,255,255,255);
	Color32 colorGris = new Color32(100,100,100,255);

	GameObject npc;
	
	void Awake(){
		anim = GetComponent<Animator>();
	}
	
	void Update(){
		if(Input.GetKeyDown(teclaAvanzarDialogo) && comenzarConversacion == false){
			AvanzarDialogo();
		}
	}
	
	public bool GetReactivarConversacion(){
		return reactivaConversacion;
	}
	
	IEnumerator reactivarConversacion(){
		//Reactiva el movimiento del jugador
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
		StopCoroutine(corrutinaActual);
		jugador.GetComponent<MovementCharacter>().enabled = true;
		jugador.GetComponent<Animator>().enabled = true;
		if(npc.GetComponent<SistemaDialogosActivador>().GetconversacionOpcionalAcabada() == true || (npc.GetComponent<SistemaDialogosActivador>().conversaccionOpcional == false && npc.GetComponent<SistemaDialogosActivador>().GetconversacionPrincipalAcabada() == true)){
			//Ejecuta animacion para quitar cuadro
			npc.GetComponent<MostrarCuadroInteraccion>().EjecutarAnimacion("CuadroInteraccion",-1.0f,1.0f);
			npc.GetComponent<MostrarCuadroInteraccion>().SetcuadroDesactivado(true);
		}
		yield return new WaitForSeconds(0.5f);
		//Reactiva el sistema de dialogos
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
		reactivaConversacion = true;
		gameObject.SetActive(false);
	}
	
	//Obtiene la lista de dialogos usada durante la conversacion y hace las asignaciones de los parametro del NPC
	public void ObtenerListaDeDialogos(int personaje, Sprite retratoNPC, UnityEvent eventoAlTerminarDialogo, GameObject refJugador, GameObject refNpc, string nomNPC = "npc"){
		bool finObtencionDialogos = false;

		nombreNpc = nomNPC;

		reactivaConversacion = false;
		comenzarConversacion = false;
		indiceDialogoNPC = 0;
		indiceDialogoJugador = 0;
		referenciaSpriteNPC= retratoNPC;
		eventoTerminarDialogo = eventoAlTerminarDialogo;

		transform.Find("Canvas/NPC").gameObject.GetComponent<Image>().sprite = referenciaSpriteNPC;

		npc = refNpc;
			Debug.Log(Application.dataPath);

		jugador = refJugador;
		jugador.GetComponent<MovementCharacter>().enabled = false;
		jugador.GetComponent<Animator>().enabled = false;

		textoUI.text = "";	
		using(var reader = new StreamReader( Path.Combine(Application.streamingAssetsPath, nombreArchivoDialogos)))
		{
			var line = reader.ReadLine();
			while (!reader.EndOfStream && finObtencionDialogos == false)
			{
				line = reader.ReadLine();
				var values = line.Split(';');
				switch(idiomaActual){
					case Idioma.Espaniol:
						if((values[(personaje*6)] !="" || values[(personaje*6) + 3] != "")){
							dialogosNPC.Add(values[(personaje*6)]);
							dialogosPersonaje.Add(values[(personaje*6) + 3]);
						}else{
							finObtencionDialogos = true;
						}
					break;
					
					case Idioma.Ingles:
						if(values[(personaje*6) + 1] !="" || values[(personaje*6) + 4] != ""){
							dialogosNPC.Add(values[(personaje*6) + 1]);
							dialogosPersonaje.Add(values[(personaje*6) + 4]);
						}else{
							finObtencionDialogos = true;
						}
					break;
					
					case Idioma.Valenciano:
						if(values[(personaje*6) + 2] !="" || values[(personaje*6) + 5] != ""){
							dialogosNPC.Add(values[(personaje*6) + 2]);
							dialogosPersonaje.Add(values[(personaje*6) + 5]);
						}else{
							finObtencionDialogos = true;
						}
					break;
				}
			}
		}
		

		
		//Lo llama una vez para iniciar la animacion que ejecutara el dialogo
		EjecutarAnimacion("IntroduccionDialogo");
		//AvanzarDialogo();
	}
	
	public void EjecutarAnimacion(string nombre){
		anim.Play(nombre, 0, 0.0f);
	}

	//Avanza en el dialogo
	public void AvanzarDialogo(){
		if((indiceDialogoNPC < dialogosNPC.Count && dialogosNPC[indiceDialogoNPC]!="") || (indiceDialogoJugador < dialogosPersonaje.Count && dialogosPersonaje[indiceDialogoJugador] != "")){
			if(dialogosNPC[indiceDialogoNPC]==""){
				textoNombreUI.text = nombreProtagonista;
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
				textoNombreUI.text = nombreNpc;
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
					textoNombreUI.text = nombreNpc;
					referenciaRetratoDialogoJugador.color = colorGris;
					referenciaRetratoDialogoNPC.color = colorBlanco;
					if(corrutinaActual != null)
						StopCoroutine(corrutinaActual);
					corrutinaActual = escribirAnimacion(dialogosNPC[indiceDialogoNPC]);
					StartCoroutine(corrutinaActual);
					hablaNPC = false;
				}else{
					textoNombreUI.text = nombreProtagonista;
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
