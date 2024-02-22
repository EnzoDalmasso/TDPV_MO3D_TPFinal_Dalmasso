using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 8.0f;
    public float velocidadrotate = 100.0f;
    private Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float salto = 10f;
    public bool puedoSaltar;

    public bool estoyAtacando;
    public bool avanzoSolo;
    public float impulsoGolpe = 10f;

    public int hp;
    public int danioPunio;

    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!estoyAtacando)
        {
            
            transform.Rotate(0, x * velocidadrotate * Time.deltaTime  , 0);
            transform.Translate(0,0,y*Time.deltaTime*velocidad);
           
        }
        if (avanzoSolo)
        {
            rb.velocity = transform.forward * impulsoGolpe;
        }

    }


    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0)&& puedoSaltar&& !estoyAtacando)
        {
            anim.SetTrigger("golpeo");
            estoyAtacando= true;
        }
      
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        if (puedoSaltar) 
        {
            if (!estoyAtacando)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("salte", true);
                    rb.AddForce(new Vector3(0, salto, 0), ForceMode.Impulse);
                }
                
            }
            anim.SetBool("tocoSuelo", true);
        }
        else
        {
            estoyCayendo();
        }

        
    }

    public void estoyCayendo()
    {
        anim.SetBool("tocoSuelo", false);
        anim.SetBool("salte", false);
    }

    public void DejeDeGolpear() 
    { 
        estoyAtacando = false;
     
    }

    public void AvanzoSolo()
    {

        avanzoSolo = true;
    }

    public void DejoDeAvanzar()
    {

        avanzoSolo = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "arma")
        {
            Debug.Log("Daño");
            hp -= danioPunio;
        }
        if (hp <= 0)
        {
            //Destroy(gameObject);
        }
    }

}

