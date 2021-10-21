using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SistemaDialogos : MonoBehaviour
{
	public bool espaniol = false;
	public int personaje = 0;
	
	List<string> dialogosNPC = new List<string>();
	List<string> dialogosPersonaje = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
		using(var reader = new StreamReader(@"C:\Users\al394516\Documents\GitHub\Una-semana-solitaria\Assets\Dialogos\dialogos prueba.csv"))
		{
			var line = reader.ReadLine();
			while (!reader.EndOfStream)
			{
				line = reader.ReadLine();
				var values = line.Split(';');
				if(espaniol && (values[(personaje*4)] !="" || values[(personaje*4) + 2] != "")){
					dialogosNPC.Add(values[(personaje*4)]);
					dialogosPersonaje.Add(values[(personaje*4) + 2]);
				}else if(values[(personaje*4) + 1] !="" || values[(personaje*4) + 3] != ""){
					dialogosNPC.Add(values[(personaje*4) + 1]);
					dialogosPersonaje.Add(values[(personaje*4) + 3]);
				}
			}
		}
		
		for(int i = 0; i< Math.Max(dialogosNPC.Count,dialogosPersonaje.Count) && (dialogosNPC[i]!="" || dialogosPersonaje[i] != ""); i++){
			if(dialogosNPC[i]!="")
				Debug.Log("Conchi: " + dialogosNPC[i]);
			if(dialogosPersonaje[i] != "")
				Debug.Log("F: " + dialogosPersonaje[i]);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
