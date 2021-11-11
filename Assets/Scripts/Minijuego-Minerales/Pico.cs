using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pico : MonoBehaviour
{
    public Texture2D cursorPico;
 
    void Start()
    {
        //Cursor.visible = false;
        Cursor.SetCursor(cursorPico, Vector2.zero, CursorMode.ForceSoftware);
    }

}
