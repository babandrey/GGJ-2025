using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int growAmount;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Slimelett"))
        {
            collision.gameObject.GetComponent<ISizeable>().Resize(growAmount);
            Destroy(this.gameObject);
        }
    }
}
