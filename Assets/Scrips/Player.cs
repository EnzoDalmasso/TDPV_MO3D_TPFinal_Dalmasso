using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 8.0f;//Velocidad del Player para caminar 
    public float velocidadrotate = 100.0f;//Velocidad del Player para rotar
    public Animator anim;//Animaciones
    public float x, y;//Variables que se utilizan para poder mover el personaje en Ejes X e Y

    public Rigidbody rb;//Variable para fisicas
    public float salto = 10f;//Variable para generar un salto
    public bool puedoSaltar;//Booleano para revisar si se puede saltar 

    public bool estoyAtacando;//Booleano para revisar si se puede golpear


    public int hp;//Variable de vida del personaje
    public int danioPunio;//Variable de daño que genera el personaje al golpear 

  
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
            transform.Rotate(0, x * velocidadrotate * Time.deltaTime  , 0);
            transform.Translate(0,0,y*Time.deltaTime*velocidad);
           
        }
        

    }


    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0)&& puedoSaltar&& !estoyAtacando)//Si se detecta el click, el personaje no esta saltando y no esta atacando
        {
            anim.SetTrigger("golpeo");//Llamamos a la animacion de ataque 
            estoyAtacando= true;//Ataque es verdadero
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

      

        // Oculta el cursor del mouse
        Cursor.visible = false;

        // Centra el cursor en la pantalla
        Cursor.lockState = CursorLockMode.Locked;
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
            Debug.Log("PUÑO");
            hp -= danioPunio;
        }
        if (hp <= 0)
        {
            Debug.Log("MORII");
        }
    }


   

}

