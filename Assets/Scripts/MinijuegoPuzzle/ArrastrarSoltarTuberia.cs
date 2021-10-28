using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastrarSoltarTuberia : MonoBehaviour, IDragHandler, IEndDragHandler
{
	public void OnDrag(PointerEventData eventData){
		transform.position = Input.mousePosition;
		//Coge el padre y lo pone como ultima prioridad para ser renderizado el ultimo y asi la tuberia no queda detras del fondo
		transform.parent.transform.SetAsLastSibling();
	}
	
	public void OnEndDrag(PointerEventData eventData){
		RaycastHit hit;
		int layerMask = 1 << 8;
		layerMask = ~layerMask;
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)){
			 Debug.Log("Did Hit");
		}else{
			GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		}
	}
}
