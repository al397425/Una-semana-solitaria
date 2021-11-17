using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EstablezerInterfaz : MonoBehaviour
{
   //Establece la informacion
   public void EstablecerInformacion(string name, Sprite spr, string desc){
	   transform.Find("titulo").gameObject.GetComponent<TextMeshProUGUI>().SetText(name);
	   transform.Find("desc").gameObject.GetComponent<TextMeshProUGUI>().SetText(desc);
	   transform.Find("img").gameObject.GetComponent<Image>().sprite = spr;
   }
   
   public void DestruirInterfaz(){
	   Destroy(gameObject);
   }
}
