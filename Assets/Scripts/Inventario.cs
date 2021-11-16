using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    List<string> nombres = new List<string>();
	List<Sprite> sprites = new List<Sprite>();
	
	/**
	 * Añade un objeto al inventario
	 * @param objeto El objeto a añadir
	**/
	public void AgregarObjeto(string nombre, Sprite spr){
		nombres.Add(nombre);
		sprites.Add(spr);
	}
	
	/**
	 * Busca un objeto al inventario
	 * @param objeto El objeto a buscar
	**/
	public bool BuscarObjeto(string nombreObjeto){
		for(int i = 0; i < nombres.Count;i++){
			if(nombres[i] == nombreObjeto){
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
		for(int i = 0; i < nombres.Count;i++){
			if(nombres[i] == nombreObjeto){
				nombres.RemoveAt(i);
				sprites.RemoveAt(i);
				return;
			}
		}
	}
	
	/**
	 * Busca un objeto al inventario y lo borra
	 * @param objeto el objeto a buscar y eliminar
	**/
	public bool BuscarEliminarObjeto(string nombreObjeto){
		for(int i = 0; i < nombres.Count;i++){
			if(nombres[i] == nombreObjeto){
				nombres.RemoveAt(i);
				sprites.RemoveAt(i);
				return true;
			}
		}
		return false;
	}
}
