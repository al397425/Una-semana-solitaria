using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public float speed = 10.0f;
    public float random = 1.25f;
	public AudioSource audioDataCoin;
	
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3 (speed*random*Time.deltaTime,0,0);

    }

    void OnCollisionEnter2D(Collision2D Coll)
    {
        
            //gana puntos   
            if(Coll.gameObject.tag=="Player" && gameObject.name.Contains("Coin"))
            {
                //points = points+1;
				if(!audioDataCoin.isPlaying)
					audioDataCoin.Play(0);
                Destroy(gameObject);
            }
            
    }
}
