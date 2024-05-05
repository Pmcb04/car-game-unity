using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{

    private int lapsCompleted;
    public int max_laps;
    public static float totalTime;
    private bool startTimer;

    public GameObject car;
    private Rigidbody carRb;

    public TMP_Text totalTimeText;
    public TMP_Text lapTimeText;
    public TMP_Text lapsText;
    public TMP_Text velocityText;
    public TMP_Text listTimeLaps;


    void OnDisable()
    {
        PlayerPrefs.SetFloat("totalTime", totalTime);
    }

    void OnEnable()
    {
        Debug.Log("OnEnable contador get laps " + PlayerPrefs.GetInt("maxLaps"));
        max_laps =  PlayerPrefs.GetInt("maxLaps", 5);
    }

    // Start is called before the first frame update
    void Start()
    {
        totalTime = 0.0f;
        lapsCompleted = 0;
        if (max_laps == 0) max_laps = 3;
        carRb = car.GetComponent<Rigidbody>();
        startTimer = false;
        velocityText.text = "0km/h";
        totalTimeText.text = "00:00";
        lapTimeText.text = "00:00";
        lapsText.text = "0/" + max_laps;
        listTimeLaps.text = "";
        changeLapsText();
    }

    public void startContador(){
        startTimer = true;
    }

    public void stopContador(){
        startTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer){
            totalTime += Time.deltaTime;
        }
        updateTotalTimeContador();
        updateVelocity();
    }

    public float getTotalTime()
    {
        return totalTime;
    }

    void changeTotalTimeText()
    {
        int minutos = (int)totalTime / 60;
        int segundos = (int)totalTime % 60;
        totalTimeText.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    public void changeLapTimeText(float lapTime)
    {
        int minutos = (int)lapTime / 60;
        int segundos = (int)lapTime % 60;
        lapTimeText.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    void changeLapsText()
    {
        lapsText.text = lapsCompleted + "/" + max_laps;
    }

    void changeVelocityText(float velocity)
    {
        velocityText.text = velocity.ToString("0") + "km/h";
    }

    public void updateLaps()
    {
        lapsCompleted++;
        Debug.Log("updateLaps" + lapsCompleted);
        changeLapsText();
    }

    void updateTotalTimeContador()
    {
        if (totalTime > 0.0f)
        {
            changeTotalTimeText();
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

    public void addLapTimeToList(float lapNumber, float lapTime)
    {
        int minutos = (int)lapTime / 60;
        int segundos = (int)lapTime % 60;
        listTimeLaps.text += "Lap " + lapNumber  + "[" +  minutos.ToString("00") + ":" + segundos.ToString("00") + "]" + "\n";
    }
}
