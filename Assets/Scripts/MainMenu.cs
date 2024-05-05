 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 using TMPro;
 public class SceneController : MonoBehaviour
 {

    public TMP_InputField  numLaps;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {

        // Obtén el contenido del objeto TMP_Text como una cadena
        string contenidoTexto = numLaps.text;

        if (int.TryParse(contenidoTexto, out int numero))
        {
            // La conversión fue exitosa, 'numero' contiene el valor entero
            Debug.Log("El número es: " + numero);
            PlayerPrefs.SetInt("maxLaps", int.Parse(numLaps.text));
        }
        else
        {
            // La conversión falló, el contenido del texto no es un número válido
            Debug.LogError("El contenido del texto no es un número válido.");
        }        
    }

    public void LoadMenuScene() {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene() {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }
}