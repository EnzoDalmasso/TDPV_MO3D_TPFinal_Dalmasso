using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject PausaMenu;//Creo Gameobject
    private bool EscPause = false;//Creo un booleano igualado a falso


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))//Si se presiona la tecla P 
        {
            if (EscPause) 
            {
                Reanudar();//Llama a la funcion reanudar juego
                Debug.Log("Return");
            }
            else
            {
                Pause();//Llama a la funcion pausar juego
                Debug.Log("Pause");
            }
        }
    }


    //Esta funcion se utiliza para pausar el juego
    public void Pause()
    {
        PausaMenu.SetActive(true);//Activa menu pausa
        EscPause = true;//Avisa que el juego esta en pausa
        Time.timeScale = 0.0f;//Establece el tiempo en 0, hace que todo se congele
        Cursor.lockState = CursorLockMode.None; // libero el cursor
        Cursor.visible = true;//Activamos el cursor para que sea visible
    }

    //Esta función se utiliza cuando se quiere salir del menu pausa.
    public void Reanudar()
    {
        EscPause = false;//Avisa que el juego no esta en pausa
        Time.timeScale = 1.0f;//Reanuda el tiempo
        PausaMenu.SetActive(false);//Desactiva el menu pausa
        Cursor.visible = false;//Quita el cursor para que no se vea
    }

    //Esta funcion se utiliza para reiniciar el nivel 1
    public void ReiniciarLvl1()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);//usamos este metodo para que devuelva el nombre de la scena tambien se puede usar el numero es sola otra forma de acerlo

    }

    //Esta funcion se utiliza para reiniciar el nivel 2
    public void ReiniciarLvl2()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);//usamos este metodo para que devuelva el nombre de la scena tambien se puede usar el numero es sola otra forma de acerlo

    }

    //Esta funcion avisa que se salio del juego.
    public void Quitar()
    {

        Debug.Log("Salir");
    }
}
