using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrastrarSoltarTuberia : MonoBehaviour, IDragHandler, IEndDragHandler
{
	BoxCollider2D colision;
	Collider2D obejtoColisionado;
	
	
	void Start(){
		colision = GetComponent<BoxCollider2D>();
	}

	public void OnDrag(PointerEventData eventData){
		transform.position = Input.mousePosition;
		//Coge el padre y lo pone como ultima prioridad para ser renderizado el ultimo y asi la tuberia no queda detras del fondo
		transform.parent.transform.SetAsLastSibling();
		colision.isTrigger = true;
		
	}
	
	public void OnEndDrag(PointerEventData eventData){
		if(obejtoColisionado != null){
			Transform padre = transform.parent.transform;
			transform.SetParent(obejtoColisionado.transform.parent.transform);
			obejtoColisionado.transform.SetParent(padre);
			
			obejtoColisionado.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		}
		
		colision.isTrigger = false;
		GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
	}

	void OnTriggerStay2D(Collider2D other){
		if(colision.isTrigger == true)
			obejtoColisionado = other;
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(colision.isTrigger == true)
			obejtoColisionado = null;
	}

}
