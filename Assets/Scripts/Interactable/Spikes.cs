using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController babi))
        {
            babi.Kill();
            // TODO: Sound
        }
        else if(collision.TryGetComponent(out Slimelet slimelet))
        {
            slimelet.Kill();
            // TODO: Sound
        }
    }
}
