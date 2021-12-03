using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class SistemaDialogosActivador : MonoBehaviour
{
	//Referencia al sistema de dialogos empleado
	public Sprite retratorDelDialogo;
	public SistemaDialogos sistemaDialogos;
	public int numeroDialgoParaActivar;
	public bool activador;
	//Tras acabar la primera conversacion se puede mostrar una conversacci�nOpcional
	public bool conversaccionOpcional = true;
	public bool conversaccionOpcionalSeRepite = false; 	
	public int numeroDialgoOpcionalParaActivar;
	//Evento a llamar
	public UnityEvent eventoAlEmpezarDialogo;
	//Evento a llamar
	public UnityEvent eventoAlTerminarDialogo;
	
	bool conversacionPrincipalAcabada = false; 
	bool conversacionOpcionalAcabada = false; 

	bool activarDialogo = false;
	
void Update(){
	if(Input.GetKeyDown(sistemaDialogos.teclaDeInteraccion) && sistemaDialogos.GetReactivarConversacion() == true){
		activarDialogo = true;
	}
}

	void OnTriggerStay2D(Collider2D other){
		if ((activador == true || activarDialogo == true) && sistemaDialogos.GetcomenzarConversacion() == true && sistemaDialogos.GetReactivarConversacion() == true && conversacionOpcionalAcabada == false)
        {
			activarDialogo = false;
			if(eventoAlEmpezarDialogo != null){
				eventoAlEmpezarDialogo.Invoke();
			}
			
			if(conversacionPrincipalAcabada == false){
				sistemaDialogos.gameObject.SetActive(true);
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoParaActivar,retratorDelDialogo,eventoAlTerminarDialogo);
				conversacionPrincipalAcabada = true;
			}else if(conversaccionOpcional == true && conversacionOpcionalAcabada == false){
				sistemaDialogos.gameObject.SetActive(true);
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoOpcionalParaActivar,retratorDelDialogo,eventoAlTerminarDialogo);
				conversacionOpcionalAcabada = true;
			}
			
			if(conversaccionOpcionalSeRepite == true)
				conversacionOpcionalAcabada = false;
			
		}
		
	}
}