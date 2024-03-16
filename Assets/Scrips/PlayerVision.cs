using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    public float SensivilityMouse = 60f;//Determina la sensibilidad del 

    public float LimitVisorMin = -30f;//Determina la sensibilidad minima del mouse
    public float LimitVisorMax = 30f;//Determina la sensibilidad maxima del mouse



    public Transform player;

    float RotationX = 0;//almacena la rotación vertical del visor

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//bloqueo el cursor del ratón en el centro de la pantalla

    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * SensivilityMouse * Time.deltaTime;//Obtiene el movimiento del mouse en el eje horizontal y lo multiplica por la sensibilidad del mouse y el tiempo transcurrido
        float mouseY = Input.GetAxis("Mouse Y") * SensivilityMouse * Time.deltaTime;//Obtiene el movimiento del mouse en el eje vertical y lo multiplica por la sensibilidad del ratón y el tiempo transcurrido desde el último fotograma.

        RotationX -= mouseY;//Actualiza la variable RotationX restando el movimiento vertical del mouse. Sirve para que el visor se incline hacia arriba o hacia abajo
        RotationX = Mathf.Clamp(RotationX, LimitVisorMin, LimitVisorMax);//Limita el valor de RotationX dentro del rango. Esto asegura que el visor no pueda girar más allá de ciertos límites verticales.

        transform.localRotation = Quaternion.Euler(RotationX, 0, 0);//Aplica la rotación al objeto, ajusta la rotación vertical del visor.


        player.Rotate(Vector3.up * mouseX);//Aplica la rotación horizontal del visor al player, permite que el jugador gire horizontalmente cuando el ratón se mueve hacia los lados
    }
}

