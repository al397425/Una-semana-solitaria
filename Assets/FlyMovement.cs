using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    public float speed = 10f;
    Vector2 fly;
    // Vector2 target;
    // bool moving;
    Vector3 nextPos;
    void Start(){
        fly = new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 5f), 0);
        nextPos = new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 5f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Fly: "+ transform.position.x);
        Debug.Log("Next pos: " + nextPos);
        float step = speed * Time.deltaTime;
        if(transform.position.x != nextPos.x && transform.position.y != nextPos.y){
            transform.position = Vector3.MoveTowards(transform.position, nextPos, step);
        }
        else {
            nextPos = new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 5f), 0);
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
