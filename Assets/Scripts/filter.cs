using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Filter : MonoBehaviour
{

    public GameObject f1;
    public GameObject f2;
    public GameObject f1f2;
    public GameObject initial;
    public GameObject cur_window;
    public GameObject analyse_window;
    public GameObject confirm_window;
    public GameObject error_window;
    public GameObject circle1;
    public GameObject circle2;

    public GameObject goodFeature;
    public GameObject badFeature;

    public GameObject goodImage;

    public GameObject hair_green;
    public GameObject rarity_frame_max;


    public void MoveToScene(int sceneID){
        SceneManager.LoadScene(sceneID);
    }

    public void doExitGame() { Application.Quit(); }

    public void useFilter1(){
        f1.SetActive(true);
        f2.SetActive(false);
        f1f2.SetActive(false);
        initial.SetActive(false);
    }

    public void useFilter2(){
        f1.SetActive(false);
        f2.SetActive(true);
        f1f2.SetActive(false);
        initial.SetActive(false);
    }

    public void useFilter12(){
        f1.SetActive(false);
        f2.SetActive(false);
        f1f2.SetActive(true);
        initial.SetActive(false);
    }

    public void returnToBasic(){
        f1.SetActive(false);
        f2.SetActive(false);
        f1f2.SetActive(false);
        initial.SetActive(true);
    }

    public void analyse(){
        if (goodImage.activeSelf==true){
            analyse_window.SetActive(true);
            cur_window.SetActive(false);
        } else {
            error_window.SetActive(true);
        }
        
    }

    public void close_cur(){
        rarity_frame_max.SetActive(true);
        hair_green.SetActive(true);
        cur_window.SetActive(false);
    }

    public void confirm(){
        if ((goodFeature.activeSelf==true) && (badFeature.activeSelf==false)){
            
            confirm_window.SetActive(true);
            analyse_window.SetActive(false);
        } else {
            error_window.SetActive(true);
        }
        
    }

    public void ok(){
        error_window.SetActive(false);
    }

    public void feature1(){
        if (circle1.activeSelf==false){
            circle1.SetActive(true);
        } else {
            circle1.SetActive(false);
        }
        
    }

    public void feature2(){
        if (circle2.activeSelf==false){
            circle2.SetActive(true);
        } else {
            circle2.SetActive(false);
        }
    }

}
