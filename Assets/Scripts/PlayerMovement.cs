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
    public GameObject mosca;
    public int TiempoMoscaCazada = 2;
    public bool FlyDead = false;
    // float MoscaCazada = 0.0f;

    public int MoscasCazadas;
    int Moscas;
    Vector2 lastMovedPos;
    // Vector2 target;
    public float TiempoAparicionMoscas = 5.0f;
    float TiempoReinicio;
    Quaternion rotacionInicial;
    Vector3 puntoAparicion;
    Animator animacion;
    // Fix to GetMouseButtonDown problem
    bool AbleToHeld;
    bool Locked;
    // Seconds to respond
    public float TimeHeld;
    float CurrentTimeHeld;
    
    void Start(){
        TiempoReinicio = 0.0f;
        rotacionInicial = transform.rotation;
        Moscas = 1;
        MoscasCazadas = 0;
        //Cursor.visible = false;
        AbleToHeld = true;
        Locked = false;
        TimeHeld = 0.3f;
        CurrentTimeHeld = 0.0f;
    }
    IEnumerator MoscaParada(GameObject matamoscas)
    {
        //Wait for 2 seconds
        yield return new WaitForSecondsRealtime(matamoscas.GetComponent<FlyMovement>().TiempoMoscaCazada);
        // Debug.Log("Ha desaparecido la mosca");
        Destroy(matamoscas);
    }
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.mousePosition);
        lastMovedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, lastMovedPos, step);
    }
    // void OnTriggerEnter(Collider matamoscas){
    //     ProcessCollision(matamoscas.gameObject);
    //     Debug.Log("Esta al alcance la mosca");
    // }
    void OnTriggerStay(Collider matamoscas){
        ProcessCollision(matamoscas.gameObject);
        Debug.Log("Esta al alcance la mosca");
    }
    // void OnCollisionEnter(Collider matamoscas){
    //     ProcessCollision(matamoscas.gameObject);
    // }
    void ProcessCollision(GameObject collider){
        if(AbleToHeld && collider.GetComponent<FlyMovement>().isDead == false){
            collider.GetComponent<FlyMovement>().isDead = true;

            StartCoroutine(MoscaParada(collider));
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
            // Debug.Log("Aparición mosca");
            TiempoReinicio = 0.0f;
        }
        if(MoscasCazadas >= CazarXMoscas && tiempoTranscurrido.text != "Segundos restantes: 0"){

            Debug.Log("¡Felicidades, has cazado 5 moscas (o incluso mas :D)!");
            // Cambiar a siguente escena
        }

        if(Input.GetMouseButton(0) && CurrentTimeHeld < TimeHeld && !Locked){
            CurrentTimeHeld += Time.deltaTime;
            AbleToHeld = true;
            if(CurrentTimeHeld >= TimeHeld) Locked = true;
        }
        else if(CurrentTimeHeld >= 0.0f){
            CurrentTimeHeld -= Time.deltaTime;
            AbleToHeld = false;
            //if(CurrentTimeHeld <= 0.0f) Locked = false;
        }
        if(Input.GetMouseButtonUp(0) && Locked){
            Locked = false;
        }
        Debug.Log(string.Format("Able to held: {0}\nCurrent Time: {1}\nLocked: {2}", AbleToHeld, CurrentTimeHeld, Locked));
        TiempoReinicio += Time.deltaTime;
    }
}
