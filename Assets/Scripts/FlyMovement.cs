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
        // Debug.Log("Fly: "+ transform.position.x);
        
        float step = speed * Time.deltaTime;
        if(transform.position.x != nextPos.x && transform.position.y != nextPos.y){
            transform.position = Vector3.MoveTowards(transform.position, nextPos, step);
            // transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, );
            // target = Quaternion.Euler(0, 0, Mathf.Atan2(fly.y - nextPos.y, fly.x - nextPos.x) * 180 / Mathf.PI);
            // Debug.Log(target);
            // transform.rotation = Quaternion.Slerp(transform.rotation.z + 4, target, step);
            
            

        }
        else {
            diff = nextPos - transform.position;
            nextPos = new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 5f), 0);
            diff.Normalize();
            Debug.Log(diff);
            float rot_z = Mathf.Atan2(diff.y,diff.x);
            transform.rotation = Quaternion.Euler(0f,0f, rot_z+90);
            
            // AnguloMosca = Mathf.Atan2(nextPos.y - fly.y, nextPos.x - fly.x) * Mathf.Rad2Deg;
            fly = nextPos;
            transform.Rotate(Vector3.forward * AnguloMosca);
            // transform.Rotate(0.0F, 0.0F, AnguloMosca);
            // RotacionMosca = new Vector3(0,0,AnguloMosca);
            // Quaternion rotatons = new 
            // Debug.Log("Rotacion: "+ AnguloMosca);
            // Debug.Log("Next pos: " + nextPos);
        }
        
    }
    public void snapRotation(){
        transform.rotation = startQuaternion;
    }
    void FixedUpdate(){
        
    }
    // void OnTriggerStay(Collider matamoscas){
    //     Debug.Log("Esta tocando el matamoscas");
    //     if(Input.GetMouseButtonDown(0)){
    //         Destroy(matamoscas.gameObject);
    //     }
    // }
    // void OnTriggerEnter(Collider matamoscas){
    //     Debug.Log("Esta tocando el matamoscas");
    //     if(matamoscas.tag == "Matamoscas"){
    //         Destroy(this);
    //     }
    // }
}
