using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    List<Slimelet> slimelets = new List<Slimelet>();
    PlayerController babi;
    [SerializeField] private float fanForce;
    [SerializeField] private float[] maxHeightForSize;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Slimelet slimelett))
        {
            if (!slimelets.Contains(slimelett))
            {
                slimelets.Add(slimelett);
            }
        }
        else if(collision.gameObject.TryGetComponent(out PlayerController player))
        {
            babi = player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == babi.gameObject)
        {
            babi = null;
        }
    }

    private void FixedUpdate()
    {
        for(int i = slimelets.Count - 1; i >= 0; i--)
        {
            Slimelet slimelet = slimelets[i];

            if(slimelet != null)
            {
                int size = slimelet.slimeletSizer.slimeSize;
                float distance = Vector2.Distance(transform.position, slimelet.transform.position);
                float appliedForce = fanForce / (1.0f + distance * distance);
                slimelet.rigidbody.AddForce(Vector2.up * (appliedForce - size * 0.3f));
            }
            else
            {
                slimelets.RemoveAt(i);
            }
        }

        if(babi != null)
        {
            int size = babi.babiSizer.slimeSize;
            float distance = Vector2.Distance(transform.position, babi.transform.position);
            float appliedForce = fanForce / (1.0f + distance * distance);
            babi.rb.AddForce(Vector2.up * (appliedForce - size * 0.3f));
        }
    }
}
