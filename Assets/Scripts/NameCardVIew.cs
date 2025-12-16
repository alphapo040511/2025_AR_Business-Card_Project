using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameCardVIew : MonoBehaviour
{
    public Canvas canvas;

    public Image displayImage;

    public TextMeshProUGUI nameText;

    public void OnEnable()
    {
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;

            if (nameText != null && canvas.worldCamera == Camera.main)
                nameText.text = "√ ±‚»≠µ !";
        }


        
    }

    public void SetColor(int i)
    {
        Color newColor = Color.black;

        if(i == 0)
            newColor = Color.white;
        else if(i == 1)
            newColor = Color.red;
        else if(i == 2)
            newColor = Color.green;
        else if(i == 3)
            newColor = Color.blue;


        if(displayImage != null)
            displayImage.color = newColor;
    }
}
