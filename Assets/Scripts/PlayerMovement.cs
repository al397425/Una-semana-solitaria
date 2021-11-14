using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
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
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.mousePosition);
        lastMovedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, lastMovedPos, step);
    }
    void OnTriggerStay(Collider matamoscas){
        Debug.Log("Esta tocando el matamoscas");
        if(Input.GetMouseButtonDown(0)){
            Destroy(matamoscas.gameObject);
        }
    }
    void FixedUpdate(){
        // Debug.Log("Tiempo: " + TiempoAparicionMoscas);
        if(TiempoReinicio >= TiempoAparicionMoscas){
            puntoAparicion = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 1);
            Instantiate(mosca, puntoAparicion, rotacionInicial);
            Debug.Log("Aparición mosca");
            TiempoReinicio = 0.0f;
        }
        TiempoReinicio += Time.deltaTime;
    }
}
