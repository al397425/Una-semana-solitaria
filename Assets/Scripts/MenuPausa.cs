using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{

    public GameObject menuPausa;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p")){
            if(menuPausa.active == false){
                menuPausa.SetActive(true);
                Debug.Log("Menu activo");
                GetComponent<MovementCharacter>().enabled=false;
                GetComponent<Animator>().enabled=false;
            }else{
                menuPausa.SetActive(false);
                Debug.Log("Menu desactivo");
                GetComponent<MovementCharacter>().enabled=true;
                GetComponent<Animator>().enabled=true;
            }
        }
    }
}
