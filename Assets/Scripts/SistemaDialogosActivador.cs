using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDialogosActivador : MonoBehaviour
{
	//Referencia al sistema de dialogos empleado
	public SistemaDialogos sistemaDialogos;
	public int numeroDialgoParaActivar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerStay2D(Collider2D other){
		if (Input.GetKeyDown(sistemaDialogos.teclaDeInteraccion) && sistemaDialogos.gameObject.active == false)
        {
			sistemaDialogos.gameObject.SetActive(true);
			sistemaDialogos.ObtenerListaDeDialogos(numeroDialgoParaActivar);
		}
	}
}
