using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonarObstaculo : MonoBehaviour
{
    public GameObject SpawnS;
    public GameObject SpawnC;
    public GameObject Obstaculo;
    public GameObject Obstaculo2;
    public GameObject Coin;
    public GameObject SpawnS2;
    Vector3 positionStone;
    Vector3 positionStone2;
    Vector3 positionCoin;
    // Start is called before the first frame update
    void Start()
    {
        //Velocidad de Spawneo
        InvokeRepeating ("SpawnerMethod",1,3.5f);
        //Coordenadas de Spawneo
        positionStone = new Vector3(50f,-3.74f,0f);
        
        //Velocidad de Spawneo
        InvokeRepeating ("SpawnerMethod2",1,3.5f);
        //Coordenadas de Spawneo
        positionStone2 = new Vector3(75f,-3.74f,0f);

        //Velocidad de Spawneo
        InvokeRepeating ("CoinMethod",1,3.5f);
        //Coordenadas de Spawneo
        positionCoin = new Vector3(29f,-2.0f,0f);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnerMethod()
    {
        SpawnS = Instantiate (Obstaculo,positionStone,Quaternion.identity) as GameObject;
    }
    void SpawnerMethod2()
    {
        SpawnS2 = Instantiate (Obstaculo2,positionStone2,Quaternion.identity) as GameObject;
    }

     void CoinMethod()
    {
        SpawnC = Instantiate (Coin,positionCoin,Quaternion.identity) as GameObject;
    }

}
