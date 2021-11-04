using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    public float speed = 10f;
    Vector2 fly;
    // Vector2 target;
    // bool moving;
    Vector2 nextPos;
    void Start(){
        fly = new Vector2(Random.Range(-9f, 9f), Random.Range(-3f, 5f));
        nextPos = new Vector2(Random.Range(-9f, 9f), Random.Range(-3f, 5f));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Fly: "+ transform.position.x);
        Debug.Log("Next pos: " + nextPos);
        float step = speed * Time.deltaTime;
        if(transform.position.x != nextPos.x && transform.position.y != nextPos.y){
            transform.position = Vector2.MoveTowards(transform.position, nextPos, step);
        }
        else {
            nextPos = new Vector2(Random.Range(-9f, 9f), Random.Range(-3f, 5f));
        }
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
