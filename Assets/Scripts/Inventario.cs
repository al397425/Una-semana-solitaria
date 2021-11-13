using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    List<GameObject> objetos = new List<GameObject>();
	
	/**
	 * Añade un objeto al inventario
	 * @param objeto El objeto a añadir
	**/
	public void AgregarObjeto(GameObject objeto){
		objetos.Add(objeto);
	}
	
	/**
	 * Busca un objeto al inventario
	 * @param objeto El objeto a buscar
	**/
	public bool BuscarObjeto(string nombreObjeto){
		for(int i = 0; i < objetos.Count-1;i++){
			if(objetos[i].name == nombreObjeto){
				return true;
			}
		}
		return false;
	}
	
	/**
	 * Elimina un objeto al inventario
	 * @param objeto El objeto a eliminar
	**/
	public void EliminarObjeto(string nombreObjeto){
		for(int i = 0; i < objetos.Count-1;i++){
			if(objetos[i].name == nombreObjeto){
				objetos.RemoveAt(i);
				return;
			}
		}
	}
	
	/**
	 * Busca un objeto al inventario y lo borra
	 * @param objeto el objeto a buscar y eliminar
	**/
	public bool BuscarEliminarObjeto(string nombreObjeto){
		for(int i = 0; i < objetos.Count-1;i++){
			if(objetos[i].name == nombreObjeto){
				objetos.RemoveAt(i);
				return true;
			}
		}
		return false;
	}
}
