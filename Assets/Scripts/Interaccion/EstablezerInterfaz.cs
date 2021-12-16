using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EstablezerInterfaz : MonoBehaviour
{
   GameObject jugador;
   //Establece la informacion
   public void EstablecerInformacion(string name, Sprite spr, string desc){
	   transform.Find("Cuadro/titulo").gameObject.GetComponent<TextMeshProUGUI>().SetText(name);
	   transform.Find("Cuadro/desc").gameObject.GetComponent<TextMeshProUGUI>().SetText(desc);
	   transform.Find("CuadroImg/img").gameObject.GetComponent<Image>().sprite = spr;
   }
   
   public void DestruirInterfaz(){
	   Destroy(gameObject);
   }
   
   public void Setjugador(GameObject jug){
      jugador = jug;
   }

   public void HabilitarJugador(bool valor){
      jugador.GetComponent<MovementCharacter>().enabled = valor;
		jugador.GetComponent<Animator>().enabled = valor;
   }
}
