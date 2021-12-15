using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjecutarFundido : MonoBehaviour
{
     private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void EjecutarFadeOut(){
        if (anim != null){
                anim.SetFloat("Velocidad", -1.0f);
                anim.Play("Fade", 0, 1.0f);
        }
    }

    public void EjecutarFadeIn(){
        if (anim != null){
                anim.SetFloat("Velocidad", 1.0f);
                anim.Play("Fade", 0, 0.0f);
        }
    }
}
