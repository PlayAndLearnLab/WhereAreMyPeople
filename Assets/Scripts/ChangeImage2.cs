using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeImage2 : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] imageOptions;
    public Slider progressBar;
    // public Text messageText; // UI Text element to display "yes" or "no" message
    private int currentIndex = 0;
    private int correctAnswers = 0;
    public GameObject messageFail;
    public GameObject messageWin;


    public void OnAnswerSelectedTrue()
    {
        print(currentIndex);
        if (imageOptions.Length == 0 || currentIndex >= imageOptions.Length) return;

        if (SceneManager.GetActiveScene().name == "Teach1"){
            if (currentIndex == 1 || currentIndex == 4 || currentIndex == 5){
                correctAnswers++;
                progressBar.value = (float)correctAnswers / imageOptions.Length;
            }
        } 

        if (SceneManager.GetActiveScene().name == "Teach2"){
            if (currentIndex == 2 || currentIndex == 5 || currentIndex == 7){
                correctAnswers++;
                progressBar.value = (float)correctAnswers / imageOptions.Length;
            }
        }

        

        NextImage();
    }

    public void OnAnswerSelectedFalse()
    {
        
        if (imageOptions.Length == 0 || currentIndex >= imageOptions.Length) return;

        if (SceneManager.GetActiveScene().name == "Teach1"){
            if (currentIndex == 0 || currentIndex == 2 || currentIndex == 3){
                correctAnswers++;
                progressBar.value = (float)correctAnswers / imageOptions.Length;
            }
        } 

        if (SceneManager.GetActiveScene().name == "Teach2"){
            if (currentIndex == 0 || currentIndex == 1 || currentIndex == 3 || currentIndex == 4 || currentIndex == 6){
                correctAnswers++;
                progressBar.value = (float)correctAnswers / imageOptions.Length;
            }
        }

        NextImage();
    }

    private void NextImage()
    {
        print(currentIndex);
        currentIndex++;
        print(currentIndex);

        if (currentIndex < imageOptions.Length)
        {
            targetImage.sprite = imageOptions[currentIndex];
        }
        else
        {
            print("end");
            // Reached end of images
            if (progressBar.value < 1f)
            {
                messageFail.SetActive(true);
                // Debug.Log("no");
                Invoke("ReloadScene", 5f); // wait 2 seconds and reload
            }
            else
            {
                messageWin.SetActive(true);
                // Debug.Log("yes");
                Invoke("LoadNextScene", 5f); // wait 2 seconds and load next
            }
        }
    }

    private void ReloadScene()
    {
        messageFail.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().name == "Teach1"){
            SceneManager.LoadScene(16);
        }

        if (SceneManager.GetActiveScene().name == "Teach2"){
            SceneManager.LoadScene(28);
        }
    }
}
