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
    Vector3 diff;
    public Quaternion startQuaternion;
    
    void Start(){
        fly = new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 5f), 0);
        nextPos = new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 5f), 0);
        transform.rotation = startQuaternion;
    }
    Vector3 RotacionMosca;
    public float AnguloMosca;

    // Update is called once per frame
    void Update()
    {        
        float step = speed * Time.deltaTime;
        if(transform.position.x != nextPos.x && transform.position.y != nextPos.y){
            transform.position = Vector3.MoveTowards(transform.position, nextPos, step);
        }
        else {
            nextPos = new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 5f), 0);
            diff = nextPos - transform.position;
            // Debug.Log(diff);
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            // Debug.Log("Rotacion: "+ rot_z);
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z-90);
            fly = nextPos;
            transform.Rotate(Vector3.forward * AnguloMosca);
        }
        
    }
    public void snapRotation(){
        transform.rotation = startQuaternion;
    }
    void FixedUpdate(){
        
    }
}
