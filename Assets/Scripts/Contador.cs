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

    public GameObject car;
    private Rigidbody carRb;

    public TMP_Text timeText;
    public TMP_Text lapsText;
    public TMP_Text velocityText;


    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        laps = 0;
        if (max_laps == 0) max_laps = 5;
        carRb = car.GetComponent<Rigidbody>();
        startTimer = false;
        velocityText.text = "0km/h";
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "00:00";
        time += Time.deltaTime;
        updateContador();
        updateVelocity();
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

    void changeVelocityText(float velocity)
    {
        velocityText.text = velocity.ToString("0") + "km/h";
    }

    void updateContador()
    {
        if (time > 0.0f)
        {
            changetimeText();
        }
    }

    void updateVelocity()
    {
        float velocity = carRb.velocity.magnitude * 3.6f;
        if (velocity > 0.0f)
        {
            changeVelocityText(velocity);
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
