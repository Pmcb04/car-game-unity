using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScene : MonoBehaviour
{

    public TMP_Text totalTimeText;
    private float time;

    void OnEnable()
    {
        time  =  PlayerPrefs.GetFloat("totalTime");
    }
    // Start is called before the first frame update
    void Start()
    {
        int minutos = (int)time / 60;
        int segundos = (int)time % 60;
        totalTimeText.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
