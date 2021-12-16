using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParpadeoFuenteLuz : MonoBehaviour
{

    //Insensidad entre la que parpadeara
    public float intensidadMinima = 0.0f;
    public float insensidadMaxima = 3.23f;

    //Cada cuanto tiempo parpadeara las luces
    public float tiempoMinimoParpadeoActivo = 1.0f;
    public float tiempoMaximoParpadeoActivo = 5.0f;

    public float tiempoMinimoParpadeoDesactivo = 10.0f;
    public float tiempoMaximoParpadeoDesactivo = 18.0f;

    //Cada cuantos segundos dara un parpadeo
    public float tiempoParpadeo = 0.5f;

    Light luz;
    float tiempoActual;
    float tiempoParpadeActivoActual;
    float tiempoParpadeDesactivoActual;

    bool parpadeoActivo;

    void Awake(){
        luz = GetComponent<Light>();
        luz.intensity = insensidadMaxima;
        tiempoActual = tiempoParpadeo;
        tiempoParpadeActivoActual = 0;
        tiempoParpadeDesactivoActual = Random.Range(tiempoMinimoParpadeoDesactivo, tiempoMaximoParpadeoDesactivo);
        parpadeoActivo = false;
    }

    void Update(){
        tiempoActual -= Time.deltaTime;
        if(parpadeoActivo == true){
            if(tiempoParpadeActivoActual > 0){
                tiempoParpadeActivoActual -= Time.deltaTime;
                if ( tiempoActual < 0)
                {
                    tiempoActual = tiempoParpadeo;
                    CambiarIntensidadLuz();
                }
            }else{
                parpadeoActivo = false;
                tiempoParpadeDesactivoActual = Random.Range(tiempoMinimoParpadeoDesactivo, tiempoMaximoParpadeoDesactivo);
                luz.intensity = insensidadMaxima;
            }
        }else{
            if(tiempoParpadeDesactivoActual > 0){
                tiempoParpadeDesactivoActual -= Time.deltaTime;
            }else{
                tiempoParpadeActivoActual = Random.Range(tiempoMinimoParpadeoActivo, tiempoMaximoParpadeoActivo);
                parpadeoActivo = true;
            }
        }
        
    }

    void CambiarIntensidadLuz(){
        luz.intensity = Random.Range(intensidadMinima, insensidadMaxima);
    }
}
