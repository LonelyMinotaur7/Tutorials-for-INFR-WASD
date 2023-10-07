using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    public static int points;
    
    private void OnTriggerEnter(Collider other)
    {
        
        
        
        
        if (other.transform.tag == "Player")
        {
            Destroy(gameObject);
            points = points + 1;
            Debug.Log(points);
        }
        
    }
}
