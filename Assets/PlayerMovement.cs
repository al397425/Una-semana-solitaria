using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Vector2 lastClickedPos;
    // Vector2 target;
    bool moving;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mousePosition);
        lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
        // if (Input.GetMouseButtonDown(0)){
        //     // target = new Vector2(lastClickedPos.x, lastClickedPos.y);
        //     moving = true;
        // }
        // if(moving && (Vector2)transform.position != lastClickedPos){
        //     float step = speed * Time.deltaTime;
        //     transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
        // }
        // else{
        //     moving = false;
        // }
    }
}
