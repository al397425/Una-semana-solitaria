using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public AudioSource audioDataCoin;
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "obstaculo-pescar":
                if (!audioDataCoin.isPlaying)
                    audioDataCoin.Play(0);
                this.gameObject.SetActive(false);
                break;
        }
        
    }
}
