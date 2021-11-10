using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordesCamara: MonoBehaviour
{

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    public float xbound;
    public float ybound;
    public float fondoMarino;

    // Use this for initialization
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void LateUpdate()
    {


        //Vector3 viewPos = transform.position;
        //viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        //viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        //transform.position = viewPos;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xbound, xbound),(Mathf.Clamp(transform.position.y, -fondoMarino, ybound)));
    }
}