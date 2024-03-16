using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
 
    public float velocidad = 5.0f;//Velocidad del Player para caminar 
    public Animator anim;//Animaciones
    public float x, y;//Variables que se utilizan para poder mover el personaje en Ejes X e Y

    public Rigidbody rb;//Variable para fisicas
    public float salto = 10f;//Variable para generar un salto
    public bool puedoSaltar;//Booleano para revisar si se puede saltar 

    public bool estoyAtacando;//Booleano para revisar si se puede golpear


    public float hpMax;//Variable de vida maxima del personaje
    public float danioPunio;//Variable de da�o que genera el personaje al golpear 

    public Image barraVida;


    void Start()
    {
        puedoSaltar = false;//Se inicializa la variable de saltar en false
        anim = GetComponent<Animator>();//Inicicializo variables de animaciones
        rb = GetComponent<Rigidbody>();//Inicializo las variables de fisicas del personaje
        
    }

    void FixedUpdate()
    {
        if (!estoyAtacando)//Si el personaje no esta atacando
        {
            //Se puede mover hacia los costados o hacia adelante u atras
            transform.Translate(x * Time.deltaTime * velocidad, 0, y * Time.deltaTime * velocidad);
           
        }
        

    }


    // Update is called once per frame
    void Update()
    {

            hpMax = Mathf.Clamp(hpMax, 0, 100);
            barraVida.fillAmount = hpMax/14;

            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Mouse0) && puedoSaltar && !estoyAtacando)//Si se detecta el click, el personaje no esta saltando y no esta atacando
            {
            
                anim.SetTrigger("golpeo");//Llamamos a la animacion de ataque 
                estoyAtacando = true;//Ataque es verdadero
            }

            anim.SetFloat("VelX", x);
            anim.SetFloat("VelY", y);


            if (puedoSaltar) //El personaje puede saltar
            {
                if (!estoyAtacando)//Si no esta atacando 
                {
                    if (Input.GetKeyDown(KeyCode.Space))//Se detecta el boton space
                    {
                        anim.SetBool("salte", true);//Se activa la animacion de salto
                        rb.AddForce(new Vector3(0, salto, 0), ForceMode.Impulse);//Realiza un salto hacia arriba
                    }

                }
                anim.SetBool("tocoSuelo", true);//Activo booleano en caso que el personaje este tocando el suelo nuevamente
            }
            else
            {
                estoyCayendo();
            }



     
    }
       

    //Esta funcion se utiliza para saber si el personaje todavia sigue en el aire 
    public void estoyCayendo()
    {
        //Si el personaje no toco el suelo y no se presiono el space es por que esta en el aire todavia
        anim.SetBool("tocoSuelo", false);
        anim.SetBool("salte", false);
    }

    //Esta funcion se utiliza para que el personaje deje de golpear con la espada
    public void DejeDeGolpear() 
    { 
        estoyAtacando = false;
     
    }

    
    private void OnTriggerEnter(Collider other)
    {   //Si el zombi golpea con sus manos y colisiona con el personaje
        if (other.gameObject.tag == "manos")
        {
            //Le va a quitar vida al Player
            hpMax -= danioPunio;
            

        }
        if (hpMax <= 0)
        {
            Destroy(gameObject);
        }

    }

  
 
   

}

