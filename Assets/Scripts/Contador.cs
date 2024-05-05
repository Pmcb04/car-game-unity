using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{

    private int laps;
    public int max_laps;
    private float time;
    private bool startTimer;

    public TMP_Text timeText;
    public TMP_Text lapsText;


    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        laps = 0;
        if (max_laps == 0) max_laps = 5;
        startTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "00:00";
        time += Time.deltaTime;
        updateContador();
    }

    void changetimeText()
    {
        int minutos = (int)time / 60;
        int segundos = (int)time % 60;
        timeText.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    void changeLapsText()
    {
        lapsText.text = laps + "/" + max_laps;
    }

    void updateContador()
    {
        if (time > 0.0f)
        {
            changetimeText();
        }
    }

    public void incrementContador()
    {          
        Debug.Log("Incrementando contador ----------------------------------------");  
        laps++;
        changeLapsText();
        timeText.text = "00:00";
        if (laps == max_laps){
                lapsText.text = "FINISH";
        }

    }

}
