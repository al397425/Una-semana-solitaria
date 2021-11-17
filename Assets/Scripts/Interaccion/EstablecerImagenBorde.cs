using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstablecerImagenBorde : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
