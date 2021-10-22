using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

	bool conversacionPrincipalAcabada = false; 
	bool conversacionOpcionalAcabada = false; 
	
	void OnTriggerStay2D(Collider2D other){
		if (Input.GetKeyDown(sistemaDialogos.teclaDeInteraccion) && sistemaDialogos.GetcomenzarConversacion() == true)
        {
			if(conversacionPrincipalAcabada == false){
				sistemaDialogos.gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoParaActivar,retratorDelDialogo);
				conversacionPrincipalAcabada = true;
			}else if(conversaccionOpcional == true && conversacionOpcionalAcabada == false){
				sistemaDialogos.gameObject.SetActive(true);
				sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoOpcionalParaActivar,retratorDelDialogo);
				conversacionOpcionalAcabada = true;
			}
			
			if(conversaccionOpcionalSeRepite == true)
				conversacionOpcionalAcabada = false;
			
		}
	}
}