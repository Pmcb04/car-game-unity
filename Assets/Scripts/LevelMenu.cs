using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void LoadTrack(string trackName) {
        SceneManager.LoadScene(
            trackName + "Track"
        );
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(
            "MainMenu"  
        );
    }



}
