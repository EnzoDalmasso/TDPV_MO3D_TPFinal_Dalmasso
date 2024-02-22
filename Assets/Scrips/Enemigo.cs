using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 1f;
    public int rutina;//Variable donde se utiliza para darle las rutinas a los enemigos
    public float cronometro;//Variable que se utiliza para el tiempo entre rutinas
    public Animator ani;//Animaciones
    public Quaternion angulo;//Se utiliza para rotar al enemigo
    public float grado;//Se utiliza para detectar el grado del anugulo

    public GameObject target;

    public int hp;
    public int danioArma;

    public bool atacando;

    public float timer = 10;

    // Start is called before the first frame update
    void Start()
    {
        ani=GetComponent<Animator>();//Inicializamos la animacion
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
   
    //Funcion donde vamos a darle los comportamientos a nuestro enemigo
    public void Comportamiento_Enmigo()
    {
        if(target !=null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 5)
            {
                ani.SetBool("run", false);
                cronometro += 1 * Time.deltaTime;
                if (cronometro >= 4)
                {
                    rutina = Random.Range(0, 2);
                    cronometro = 0;

                }
                switch (rutina)
                {
                    case 0:
                        ani.SetBool("walk", false);
                        break;
                    case 1:
                        grado = Random.Range(0, 360);
                        angulo = Quaternion.Euler(0, grado, 0);
                        rutina++;
                        break;
                    case 2:
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                        transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
                        ani.SetBool("walk", true);
                        break;

                }
            }
            else
            {
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
                else
                {
                    timer--;
                    if (timer< 0) 
                    {
                        
                        ani.SetBool("walk", false);

                        ani.SetBool("run", false);
                        ani.SetBool("attack", true);
                        atacando = true;

                    }
                        
                        
                     
                   
                }

            }
            
            
        }
      
       
    }
   
    
    void Update()
    {
        Comportamiento_Enmigo();
        
    }

    
    public void Final_Ani()

    {
        ani.SetBool("attack", false);
        atacando = false;
        timer = 10;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "arma" )
        {
            Debug.Log("Daño");
            hp -= danioArma;
        }
        if (hp <= 0)
        {
            // Destroy(gameObject);
        }
    }
    
    
   

}
