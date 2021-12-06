using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstablecerMaterial : MonoBehaviour
{
    public Color colorGeneral = new Color(1.0F, 1.0F, 1.0F, 1.0F);
    public Texture texturaBase;
    public Texture texturaNormalmap;

    Material material;
    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.color = colorGeneral;
        material.SetTexture("_MainTex", texturaBase);
        material.SetTexture("_BumpMap", texturaNormalmap);
    }
}
