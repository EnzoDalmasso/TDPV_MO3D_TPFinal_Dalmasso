using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPies : MonoBehaviour
{
    public Player player;//Clase Player


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        player.puedoSaltar = true;//Si el player esta en colisionando con el suelo puede realizar el salto 
    }

    private void OnTriggerExit(Collider other)
    {
        player.puedoSaltar = false;//Si el player esta en el aire no puede realizar otro salto 
    }

}
