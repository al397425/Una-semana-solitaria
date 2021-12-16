using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimartor : MonoBehaviour
{
    public Animator anim;
    
    public void ActivarAnimator(bool activado){
        anim.enabled = activado;
    }
}
