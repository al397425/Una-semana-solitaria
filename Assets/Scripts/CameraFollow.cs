using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    public Vector3 velocidad;
    float velocidad_inicial;
    bool abajo=false;
    public float contador = 0;
    bool terminado=false;

    private void Start()
    {
        velocidad_inicial = velocidad.y;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -18 && contador <3)                  //Abajo del todo
        {
            abajo = true;
            velocidad.y = 0;
            contador += 0.025f;
            
        }
        
        if (terminado == false)
        {
            if (abajo && contador >= 3)
            {
                velocidad.y = velocidad_inicial;
                transform.position += velocidad;            //subiendo
            }
            else if (abajo == false && contador < 3)
            {
                transform.position -= velocidad;            //bajando
            }

            if (transform.position.y > 0 && abajo)                  //FIN
            {
                terminado = true;
            }
        }
        else
        {
            velocidad.y = 0;
        }
            
        


    }

}
