using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorZombi : MonoBehaviour
{
    public GameObject objetoGenerar;//Objeto que deseo generar
    public Vector3 spawnAreaCenter;//Variable que indica el centro del spawner
    public Vector2 spawnAreaSize;//Tamaño del area del spawn de zombis
    public int numeroZombis;//Esta variable se utiliza para saber cuantos prebas se querran generar

    private void Start()
    {
        GeneratePrefabs();
    }

    private void GeneratePrefabs()
    {
        for (int i = 0; i < numeroZombis; i++)//Genera la cantidad de zombis mencionada en el inspector
        {
            //Genera diferentes puntos de spawn segun el tamaño declarado en el inspector

            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
                0f);

            //Instancia los zombis segun los puntos creados
            Instantiate(objetoGenerar, spawnPosition, Quaternion.identity);
        }
    }

}
