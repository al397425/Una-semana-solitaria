using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDialogosActivador : MonoBehaviour
{
	//Referencia al sistema de dialogos empleado
	public SistemaDialogos sistemaDialogos;
	public int numeroDialgoParaActivar;
	//Tras acabar la primera conversacion se puede mostrar una conversacci�nOpcional
	public bool conversacci�nOpcional = false; 
	[ConditionalField("conversacci�nOpcional")] public int numeroDialgoOpcionalParaActivar;

	
	void OnTriggerStay2D(Collider2D other){
		if (Input.GetKeyDown(sistemaDialogos.teclaDeInteraccion) && sistemaDialogos.GetfinConversacion() == false)
        {
			if(conversacionPrincipalAcabada == false){
				sistemaDialogos.gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoParaActivar);
			}else if(conversacci�nOpcional == true){
				sistemaDialogos.gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoOpcionalParaActivar);
			}
		}
	}
}


