using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button_color : MonoBehaviour
{
    [SerializeField] int count = 0;
    [SerializeField] Color[] colorArray;

    public void ChangeColor()
    {
        if (count<colorArray.Length){
            gameObject.GetComponent<Image>().color = new Color(colorArray[count].r,colorArray[count].g,colorArray[count].b);
            if (count == colorArray.Length -1){
                count = -1;
            }
            count += 1;
        }

    }
}
