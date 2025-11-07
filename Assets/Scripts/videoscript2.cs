using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoscript2 : MonoBehaviour
{

     VideoPlayer video;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;

         
    }


     void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        if (SceneManager.GetActiveScene().name == "OutroVideoChicken"){
            Debug.Log("scene chicken");
            SceneManager.LoadScene(25);
        } else if (SceneManager.GetActiveScene().name == "OutroVideoMom"){
            Debug.Log("scene mom");
            SceneManager.LoadScene(35);
        } else if (SceneManager.GetActiveScene().name == "OutroVideo"){
            Debug.Log("scene sleep");
            SceneManager.LoadScene(0);
        }  else if (SceneManager.GetActiveScene().name == "IntroVideoBrother"){
            Debug.Log("scene brother");
            SceneManager.LoadScene(50);
        } 
        else {
            SceneManager.LoadScene(14);
        }
    }
        
}
