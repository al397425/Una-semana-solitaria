using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerarSolucion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Accelera la solucion para comprobarla   
    **/
    public void AccelerarComprobacionSolucion(GameObject referenciaPuzzle){
        referenciaPuzzle.GetComponent<CrearPuzzle>().SetpuzzleInteractuable(false);
        Time.timeScale = 40.0f;
    }
}
