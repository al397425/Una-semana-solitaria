using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonarObstaculo : MonoBehaviour
{
    public GameObject SpawnS;
    public GameObject SpawnC;
    public GameObject Obstaculo;
    public GameObject Coin;
    Vector3 positionStone;
    Vector3 positionCoin;
    // Start is called before the first frame update
    void Start()
    {
        //Velocidad de Spawneo
        InvokeRepeating ("SpawnerMethod",1,3.5f);
        //Coordenadas de Spawneo
        positionStone = new Vector3(50f,-3.74f,0f);
        //Velocidad de Spawneo
        InvokeRepeating ("CoinMethod",1,3.5f);
        //Coordenadas de Spawneo
        positionCoin = new Vector3(23f,-2.0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(SpawnC.transform.position == new Vector3(-50f,0f,0f)){
            Destroy(SpawnC);
        }
        if(SpawnS.transform.position == new Vector3(-50f,0f,0f)){
            Destroy(SpawnS);
        }*/
    }

    void SpawnerMethod()
    {
        SpawnS = Instantiate (Obstaculo,positionStone,Quaternion.identity) as GameObject;
    }

     void CoinMethod()
    {
        SpawnC = Instantiate (Coin,positionCoin,Quaternion.identity) as GameObject;
    }

    /*void OnCollisionEnter2D(Collision2D Coll)
    {
        
            //gana puntos   
            if(Coll.gameObject.tag=="Player")
            {
                Destroy(SpawnC);
                Destroy(SpawnS);
            }
    }*/

}
