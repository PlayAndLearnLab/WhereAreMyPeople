using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoscript1 : MonoBehaviour
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
        SceneManager.LoadScene(48);
        //SceneManager.LoadScene(12);//the scene that you want to load after the video has ended.
    }
}
