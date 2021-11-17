using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int MoscasEnPantalla = 9;
    public int CazarXMoscas = 5;
    public float speed = 10f;
    public Text tiempoTranscurrido;
    int MoscasCazadas;
    int Moscas;
    Vector2 lastMovedPos;
    // Vector2 target;
    bool moving;
    public float TiempoAparicionMoscas = 5.0f;
    float TiempoReinicio;
    public GameObject mosca;
    Quaternion rotacionInicial;
    Vector3 puntoAparicion;
    void Start(){
        TiempoReinicio = 0.0f;
        rotacionInicial = transform.rotation;
        Moscas = 1;
        MoscasCazadas = 0;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.mousePosition);
        lastMovedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, lastMovedPos, step);
    }
    void OnTriggerEnter(Collider matamoscas){
        // Debug.Log("Esta tocando el matamoscas");
        if(Input.GetMouseButtonDown(0)){
            Destroy(matamoscas.gameObject);
            Moscas -= 1;
            MoscasCazadas += 1;
        }
    }
    void OnTriggerStay(Collider matamoscas){
        // Debug.Log("Esta tocando el matamoscas");
        if(Input.GetMouseButtonDown(0)){
            Destroy(matamoscas.gameObject);
            Moscas -= 1;
            MoscasCazadas += 1;
        }
    }
    void FixedUpdate(){
        // Debug.Log("Tiempo: " + TiempoAparicionMoscas);
        if(TiempoReinicio >= TiempoAparicionMoscas && MoscasEnPantalla > Moscas){
            puntoAparicion = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 1);
            Instantiate(mosca, puntoAparicion, rotacionInicial);
            Moscas += 1;
            Debug.Log("Aparición mosca");
            TiempoReinicio = 0.0f;
        }
        if(MoscasCazadas >= CazarXMoscas && tiempoTranscurrido.text != "Segundos restantes: 0"){
            Debug.Log("¡Felicidades, has cazado 5 moscas (o incluso mas :D)!");
            // Cambiar a siguente escena
        }
        TiempoReinicio += Time.deltaTime;
    }
}
