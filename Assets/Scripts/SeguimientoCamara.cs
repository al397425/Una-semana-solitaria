using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    [SerializeField]
    private Transform objetivo;


    public float limiteY = 2.0f;

    public Transform referenciaEscenario;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objetivo.position.x, transform.position.y, transform.position.z);
    }
}
