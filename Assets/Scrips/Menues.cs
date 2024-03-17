using UnityEngine;
using UnityEngine.SceneManagement;


public class Menues : MonoBehaviour
{
    [SerializeField] GameObject PantallaGameOver;
    [SerializeField] GameObject PantallaSiguienteNivel;

    public GameObject Jugdor;

    public GeneradorZombi referencia_Generador;


    private void Start()
    {
        PantallaGameOver.SetActive(false);//Descativamos menu game over
        PantallaSiguienteNivel.SetActive(false);//Descativamos menu siguiente nivel
        Jugdor = GameObject.Find("Player");//Inicializamos el objeto 

        referencia_Generador.actual.victoria += menuSiguienteNivel;//Llamo al menu siguiente nivel una vez eliminado a todos los enemigos


    }
    private void Update()
    {
        if (Jugdor == null)//Si el jugador no exitste llamo a la pantalla gameOver
        {

            menuGamerOver();
        }
       
      
    }

    public void menuGamerOver()//Funcion que sirve para activar la pantalla gameover
    {

        // Oculta el cursor del mouse
        Cursor.visible = true;

        // Centra el cursor en la pantalla
        Cursor.lockState = CursorLockMode.None;

        PantallaGameOver.SetActive(true);

    }


    public void menuSiguienteNivel()//Funcion que sirve para activar la pantalla siguiente nivel
    {
        Jugdor.SetActive(false);

        // Oculta el cursor del mouse
        Cursor.visible = true;

        // Centra el cursor en la pantalla
        Cursor.lockState = CursorLockMode.None;


        PantallaSiguienteNivel.SetActive(true);


    }

    //Funcion para mandar al menu inicial
    public void SalirMenu()
    {
        SceneManager.LoadScene(0);

    }

    //Esta funcion se utiliza para reiniciar el nivel 1
    public void ReintentarLvl1()
    {
        SceneManager.LoadScene(1);

    }

    //Esta funcion se utiliza para reiniciar el nivel 2
    public void ReintentarLvl2()
    {
        SceneManager.LoadScene(2);

    }

    //Esta funcion se utiliza para avanzar a la siguiente escena 
    public void siguienteNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
  
}
