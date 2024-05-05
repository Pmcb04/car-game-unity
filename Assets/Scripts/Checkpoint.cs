using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Checkpoint : MonoBehaviour
{
    [Header("Points")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints;
    
    private Contador contador;

    private float max_laps;

    [Header("Information")]
    private float currentCheckpoint;
    private float currentLap;
    
    private bool started;
    private bool finished;

    private float currentLapTime;


    private void Start()
    {
        contador = GameObject.Find("Canvas").GetComponent<Contador>();
        
        currentCheckpoint = 0;
        currentLap = 0;

        started = false;
        finished = false;

        currentLapTime = 0;

        max_laps = contador.max_laps;
    }

    private void Update()
    {
        if (started && !finished)
        {
            currentLapTime += Time.deltaTime;
            contador.changeLapTimeText(currentLapTime);    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            // Started the race
            if (thisCheckpoint == start && !started)
            {
                print("Started");
                started = true;
                currentLap = 1;
                contador.startContador();
                contador.updateLaps();
            }
            // Ended the lap or race
            else if (thisCheckpoint == end && started)
            {
                // If all the max_laps are finished, end the race
                if (currentLap == max_laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {

                        // contador.addLapTimeToList(currentLap, currentLapTime);
                        contador.stopContador();
                        finished = true;
                        SceneManager.LoadScene("FinalScene");
                        print("Finished");
                    }
                    else
                    {
                        print("Did not go through all checkpoints");
                    }
                }
                // If all max_laps are not finished, start a new lap
                else if (currentLap < max_laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        contador.addLapTimeToList(currentLap, currentLapTime);
                        currentLap++;
                        currentCheckpoint = 0;
                        currentLapTime = 0;
                        print($"Started lap {currentLap}");
                        contador.updateLaps();
                    }
                    else
                    {
                        print("Did not go through all checkpoints");
                    }
                }
            }

            // Loop through the checkpoints to compare and check which one the player touched
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                    return;

                // If the checkpoint is correct
                if (thisCheckpoint == checkpoints[i] && i + 1 == currentCheckpoint + 1)
                {
                    print($"Correct Checkpoint: {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.000}");
                    currentCheckpoint++;
                }
                // If the checkpoint is incorrect
                else if (thisCheckpoint == checkpoints[i] && i + 1 != currentCheckpoint + 1)
                {
                    print($"Incorrect checkpoint");
                }
            }
        }
    }
}