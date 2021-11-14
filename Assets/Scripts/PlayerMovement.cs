using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Vector2 lastMovedPos;
    // Vector2 target;
    bool moving;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.mousePosition);
        lastMovedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, lastMovedPos, step);
    }
    void OnTriggerEnter(Collider matamoscas){
        Debug.Log("Esta tocando el matamoscas");
        if(matamoscas.tag == "Matamoscas" && Input.GetMouseButtonDown(0)){
            Destroy(this);
        }
    }
}
