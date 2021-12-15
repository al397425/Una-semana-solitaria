using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstablecerPropiedadesAudio : MonoBehaviour
{

    AudioSource sonido;
    Animator anim;

    void Awake(){
        sonido = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void ReproducirUnaVezClip(AudioClip clip){
        sonido.PlayOneShot(clip, 1.0f);
    }

    public void ReproducirClip(AudioClip clip){
        sonido.clip = clip;
        sonido.Play();
    }
    public void EstablecerBucle(bool valor){
        sonido.loop = valor;
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
