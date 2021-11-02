using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrastrarSoltarTuberia : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler 
{
	BoxCollider2D colision;
	Collider2D obejtoColisionado;
	
	GameObject referenciaPuzzle;
	
	void Start(){
		colision = GetComponent<BoxCollider2D>();
	}

	public void OnPointerDown (PointerEventData eventData){
        referenciaPuzzle.GetComponent<CrearPuzzle>().GetfuenteAudio().clip = referenciaPuzzle.GetComponent<CrearPuzzle>().sonidoCogerTuberia;
		referenciaPuzzle.GetComponent<CrearPuzzle>().GetfuenteAudio().Play(0);
    }

	public void OnPointerUp(PointerEventData eventData){
		referenciaPuzzle.GetComponent<CrearPuzzle>().GetfuenteAudio().clip = referenciaPuzzle.GetComponent<CrearPuzzle>().sonidoSoltarTuberia;
		referenciaPuzzle.GetComponent<CrearPuzzle>().GetfuenteAudio().Play(0);
	}

	public void OnDrag(PointerEventData eventData){
		if(GetComponent<TipoTuberia>().GetpuedeMoverse() == true){
			transform.position = Input.mousePosition;
			//Coge el padre y lo pone como ultima prioridad para ser renderizado el ultimo y asi la tuberia no queda detras del fondo
			transform.parent.transform.SetAsLastSibling();
			colision.isTrigger = true;
		}
	}
	
	public void OnEndDrag(PointerEventData eventData){
		if(obejtoColisionado != null && GetComponent<TipoTuberia>().GetpuedeMoverse() == true){
			Transform padre = transform.parent.transform;
			transform.SetParent(obejtoColisionado.transform.parent.transform);
			obejtoColisionado.transform.SetParent(padre);
			
			obejtoColisionado.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		}
		
		colision.isTrigger = false;
		GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
	}

	void OnTriggerStay2D(Collider2D other){
		if(tag == other.tag && colision.isTrigger == true && other.GetComponent<TipoTuberia>().GetpuedeMoverse() == true)
			obejtoColisionado = other;
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(colision.isTrigger == true)
			obejtoColisionado = null;
	}

	/**
	 * Establece la referencia a el puzzle para ejecutar los audios
	**/
	public void SetreferenciaPuzzle(GameObject refPuzzle){
		referenciaPuzzle = refPuzzle;
	}
}
