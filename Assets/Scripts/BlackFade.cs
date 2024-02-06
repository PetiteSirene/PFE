using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFade : UIElement
{
    //private bool isVisible;

    void Awake()
    {
        canvasR = GetComponent<CanvasRenderer>();
    }
    void Start()
    {
        canvasR.SetAlpha(0f);
    }
    
    
}
