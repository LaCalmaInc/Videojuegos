using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenySystem : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cambia "Juego" por el nombre de tu escena de juego
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego..."); // Mensaje de depuración
        Application.Quit(); // Cierra la aplicación
    }
}
