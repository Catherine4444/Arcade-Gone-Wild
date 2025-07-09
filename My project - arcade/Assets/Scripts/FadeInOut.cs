using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    public bool fadein;
    public bool fadeout;

    public float timeToFade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadein==true)
        {
            if(canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += timeToFade * Time.deltaTime; // get more and more black 
                if(canvasgroup.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
        if (fadeout==true)
        {
            Debug.Log("Fade Out Called");
            if(canvasgroup.alpha >= 0)
            {
                canvasgroup.alpha -= timeToFade * Time.deltaTime;
                if(canvasgroup.alpha == 0)
                {
                    fadeout = false;
                }
            }
        }
        
    }

    public void FadeIn()
    {
        fadein = true;
        
    }

    public void FadeOut()
    {
        fadeout = true;
    }
}
