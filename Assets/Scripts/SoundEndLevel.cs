using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEndLevel : MonoBehaviour
{
    public AudioSource audioDataFall;
    // Start is called before the first frame update
    void Start()
    {
       audioDataFall = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D Coll)
    {
    if(Coll.gameObject.tag=="Player" && gameObject.name.Contains("LimiteDeNivel"))
        {
    if(!audioDataFall.isPlaying)
		audioDataFall.Play(0);
    }
    }
}