using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
  
    //Esta funcion se utiliza para iniciar el juego en el nivel 1
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    //Esta funcion avisa que se salio del juego.
    public void Salir()
    {
        Debug.Log("SALIR");
    }

 
}
