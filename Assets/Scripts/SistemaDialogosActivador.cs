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
	//Tras acabar la primera conversacion se puede mostrar una conversacciónOpcional
	public bool conversaccionOpcional = true;
	public bool conversaccionOpcionalSeRepite = false; 	
	public int numeroDialgoOpcionalParaActivar;
	//Evento a llamar
	public UnityEvent eventoAlEmpezarDialogo;
	//Evento a llamar
	public UnityEvent eventoAlTerminarDialogo;
	
	bool conversacionPrincipalAcabada = false; 
	bool conversacionOpcionalAcabada = false; 
	
	void OnTriggerStay2D(Collider2D other){
		if (Input.GetKeyDown(sistemaDialogos.teclaDeInteraccion) && sistemaDialogos.GetcomenzarConversacion() == true)
        {
			if(eventoAlEmpezarDialogo != null){
				eventoAlEmpezarDialogo.Invoke();
			}
			
			if(conversacionPrincipalAcabada == false){
				sistemaDialogos.gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoParaActivar,retratorDelDialogo,eventoAlTerminarDialogo);
				conversacionPrincipalAcabada = true;
			}else if(conversaccionOpcional == true && conversacionOpcionalAcabada == false){
				sistemaDialogos.gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoOpcionalParaActivar,retratorDelDialogo,eventoAlTerminarDialogo);
				conversacionOpcionalAcabada = true;
			}
			
			if(conversaccionOpcionalSeRepite == true)
				conversacionOpcionalAcabada = false;
			
		}
	}
}