using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    public Transform target;
    public Vector3 velocidad;
    public Vector3 auxiliar;
    bool abajo=false;
    public bool terminado=false;
    public double contador = 0;



    private void FixedUpdate()
    {
        if (transform.position.y < -18 && contador <3)                  //Abajo del todo
        {
            abajo = true;
            velocidad = Vector3.zero;
            contador += 0.025;
            
        }
        

        if (abajo && contador >=3)
        {
            velocidad = auxiliar;
            transform.position += velocidad;            //subiendo
        }
        else if(abajo == false && contador<3)
        {
            transform.position -= velocidad;            //bajando
        }

        if (transform.position.y > 0 && abajo)                  //FIN
        {
            velocidad = Vector3.zero;
            terminado = true;
        }
        


    }

}
