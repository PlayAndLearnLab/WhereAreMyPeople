using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image targetImage; // Reference to the UI Image component
    public Sprite[] imageOptions; // Array of sprites to switch between
    private int currentIndex = 0;

    public void ChangePicture()
    {
        if (imageOptions.Length == 0 || currentIndex >= imageOptions.Length - 1) return;
        
        currentIndex = (currentIndex + 1) % imageOptions.Length;
        targetImage.sprite = imageOptions[currentIndex];
    }
}

