using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDialogosActivador : MonoBehaviour
{
	//Referencia al sistema de dialogos empleado
	public SistemaDialogos sistemaDialogos;
	public int numeroDialgoParaActivar;
	//Tras acabar la primera conversacion se puede mostrar una conversacciónOpcional
	public bool conversacciónOpcional = false; 
	[ConditionalField("conversacciónOpcional")] public int numeroDialgoOpcionalParaActivar;

	
	void OnTriggerStay2D(Collider2D other){
		if (Input.GetKeyDown(sistemaDialogos.teclaDeInteraccion) && sistemaDialogos.GetfinConversacion() == false)
        {
			if(conversacionPrincipalAcabada == false){
				sistemaDialogos.gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoParaActivar);
			}else if(conversacciónOpcional == true){
				sistemaDialogos.gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoOpcionalParaActivar);
			}
		}
	}
}


