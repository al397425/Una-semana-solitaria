using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToDestroy : MonoBehaviour
{
    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
