using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 1f;//Velocidad del zombi para caminar
    public int rutina;//Variable donde se utiliza para darle las rutinas a los enemigos
    public float cronometro;//Variable que se utiliza para el tiempo entre rutinas
    public Animator ani;//Animaciones
    public Quaternion angulo;//Se utiliza para rotar al enemigo
    public float grado;//Se utiliza para detectar el grado del anugulo

    public GameObject target;//Objeto
    

    public int hp;//Vida del Zombi
    public int danioArma;//Daño que realiza el personaje

    public bool atacando;//Booleano que se utiliza para saber si esta atacando
    public bool muerto;//Booleano que se utiliza para saber si el Zombi esta muerto

    private float clear_Cuerpo = 0;//Inicializo un timer para eliminar un cuerpo
    private float tiempo_Ataque;//Inicializo un timer para tener tiempo entre golpe y golpe

    public Enemigo actual;//Enemigo actual
    public Action morir;//accion que avisa cuando muere el enemigo actual

    bool freno_signal=false;//Booleano que se utilzia para avisarle al generador 

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();//Inicializamos la animacion
        target = GameObject.Find("Player");//Inicializamos el objeto 
        
        

    }

    private void Awake()
    {
        actual = this;
    }

    // Update is called once per frame

    //Funcion donde vamos a darle los comportamientos a nuestro enemigo
    public void Comportamiento_Enmigo()
    {
        if (target != null && !muerto)//Si el player existe y no esta muerto
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 5)//Si el Player esta en un radio mayor de 5 
            {
                ani.SetBool("run", false);//La animacion de correr se desactiva
                cronometro += 1 * Time.deltaTime;//activamos el cronometro para poder utilizarlas para las rutinas
                if (cronometro >= 4)
                {
                    rutina = Random.Range(0, 2);//La rutina va a ser igual al numero aleatorio
                    cronometro = 0;//se resetea el cronometro

                }
                switch (rutina)
                {
                    case 0://Primer caso el personaje va a estar en estado estatico
                        ani.SetBool("walk", false);
                        break;
                    case 1://Segundo caso el personaje genera una rotacion aleatortia 
                        grado = Random.Range(0, 360);//
                        angulo = Quaternion.Euler(0, grado, 0);
                        rutina++;
                        break;
                    case 2://Tercer caso el personaje va a caminar acorde la rotacion dada y va a caminar hacia adelante activando su animacion
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                        transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
                        ani.SetBool("walk", true);
                        break;

                }
            }
            else
            {
                //Si el Player esta dentro de un radio de 1 y el zombi no esta atacando
                //El zombi va a activar sus animaciones de correr y va a correr hacia el personaje
                if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
                {
                    var lookPos = target.transform.position - transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                    ani.SetBool("walk", false);

                    ani.SetBool("run", true);

                    transform.Translate(Vector3.forward * Time.deltaTime * velocidad);

                    ani.SetBool("attack", false);

                }
                //Si el Player esta en un radio menor a 1 el zombi va activar la animacion de ataque
                else if (tiempo_Ataque >= 0)
                {

                    ani.SetBool("walk", false);

                    ani.SetBool("run", false);
                    ani.SetBool("attack", true);
                    atacando = true;


                }


            }


        }


    }


    void Update()
    {
        Comportamiento_Enmigo();

        eliminar_Zombi();
        tiempo_Ataque += Time.deltaTime;//Inicializo el tiempo de ataque del zombi
      
    }

    //Esta funcion la utilizo para avisarle a la animacion de ataque que el zombi dejo de atacar
    public void Final_Ani()

    {

            ani.SetBool("attack", false);//Desactivo animacion de ataque
            atacando = false;
            tiempo_Ataque= 0;//reseteo el tiempo de ataque
   
    }


    private void OnTriggerEnter(Collider other)
    {
        
        //Si el Zombi recibe el gole con un arma(Espada)
        if (other.gameObject.tag == "arma")
        {
          
            hp -= danioArma;//Se le resta la vida
  
        }

 

        if (hp <= 0)//Si el zombi se queda sin vida
        {
            
            muerto = true;//El boleano de muerte se activa 
            rutina = 0;//Se iguala la rutina en (Estado estatico)
            ani.SetBool("muerto", true);//Se activa animacion de muerte
            ani.SetBool("attack", false);//Se desactiva animacion de ataque
            ani.SetBool("walk", false);//Se desactiva animacion de caminar
            ani.SetBool("run", false);//Se desactiva animacion de correr

        }
    

    }
    //Esta funcion se utiliza para eliminar el cuerpo del zombi
    private void eliminar_Zombi()
    {
        if (muerto==true)//Si el zombi esta muerto
        {
           

            if (freno_signal==false)
            {

                //Tiro la señal para que alguien la escuche
                actual.morir();  
                freno_signal = true;

            }
            

            clear_Cuerpo += Time.deltaTime;//Iniciamos un contador 

            if (clear_Cuerpo >= 5)//Si el contador es mayor e igual a 5 se va a eliminar el cuerpo del zombi
            {

               Destroy(gameObject);
            }

        }

       
    }




}
   
   


