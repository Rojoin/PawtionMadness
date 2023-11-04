using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Stuff : MonoBehaviour
{
    public AK.Wwise.Event Play_Agarrar_item_Porcion;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Play_Agarrar_item_Porcion.Post(gameObject); 
        }
    }
}
