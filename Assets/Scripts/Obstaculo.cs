using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public float speed = 10.0f;
    public float random = 1.25f;
        
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3 (speed*random*Time.deltaTime,0,0);
    }

}