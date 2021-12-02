using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlNivel : MonoBehaviour
{
    public void Minerales(){
        SceneManager.LoadScene("Minijuego-Minerales");
    }

    public void SeguirJugando(){
        SceneManager.LoadScene("FinalDemo");//Cambiar escena para seguir jugando
    }

}
