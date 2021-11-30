using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ColisionSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   void OnTriggerEnter2D(Collider2D Coll)
    {
        if(Coll.gameObject.tag=="Player" && gameObject.name.Contains("FlechaPueblo"))
            {
                SceneManager.LoadScene("level1");
            }
            if(Coll.gameObject.tag=="Player" && gameObject.name.Contains("ColliderPanaderia"))
            {
                //SceneManager.LoadScene("Panaderia");
            }
            if(Coll.gameObject.tag=="Player" && gameObject.name.Contains("ColliderBar"))
            {
                //SceneManager.LoadScene("Bar");
            }

            if(Coll.gameObject.tag=="Player" && gameObject.name.Contains("FlechaCama1"))
            {
                SceneManager.LoadScene("Secuencia4");
            }
            if(Coll.gameObject.tag=="Player" && gameObject.name.Contains("FlechaCama2"))
            {
                SceneManager.LoadScene("Secuencia8");
            }
    }
}
