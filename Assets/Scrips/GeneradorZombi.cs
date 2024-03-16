using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GeneradorZombi : MonoBehaviour
{
    public GameObject objetoGenerar;//Objeto que deseo generar
    public Vector3 spawnAreaCenter;//Variable que indica el centro del spawner
    public Vector3 spawnAreaSize;//Tamaño del area del spawn de zombis
    public int numeroZombis;//Esta variable se utiliza para saber cuantos prebas se querran generar

    public GeneradorZombi actual;
    public Action victoria;

    private void Start()
    {
        GeneratePrefabs();

    }
    private void Awake()
    {
        actual = this;

      
    }
    private void GeneratePrefabs()
    {
    

        for (int i = 0; i < numeroZombis; i++)//Genera la cantidad de zombis mencionada en el inspector
        {

            
            //Genera diferentes puntos de spawn segun el tamaño declarado en el inspector

            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
                Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2));

            //creo un objeto para instanciar
            GameObject ref_enemigo = Instantiate(objetoGenerar, spawnPosition, Quaternion.identity);

            //Creo una variable para guardar el scrip de ese objeto
            var ref_scrip= ref_enemigo.GetComponent<Enemigo>();

            

            //Vinculo la señal de ese objeto a una funcion que va a actuar cuando se tire la señal
            ref_scrip.actual.morir += condicionVictoria;

          
        }

       

    }

   private void condicionVictoria()
    {
   

        numeroZombis--;//Descontamos la cantidad de zombis cuando los matamos

        if (numeroZombis == 0)//Si no hay mas zombis avisa a la condicion de victoria
        {

            actual.victoria();

        }




    }




}
