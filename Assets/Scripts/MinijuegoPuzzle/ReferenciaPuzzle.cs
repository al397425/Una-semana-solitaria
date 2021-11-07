using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenciaPuzzle : MonoBehaviour
{

	public void SetRefTablero(GameObject tab){
		tab.GetComponent<DestruirPuzzle>().SetrefTablero(tab);
	}
}
