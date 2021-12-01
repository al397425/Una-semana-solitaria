using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDesactivadorObjetos : MonoBehaviour
{
    public void ActivarObjeto(GameObject obj){
		obj.active = true;
	}
	
	public void DesactivarObjeto(GameObject obj){
		obj.active = false;
	}
}
