using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    List<Slimelet> slimelets = new List<Slimelet>();
    [SerializeField] private float fanForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Slimelet slimelett))
        {
            if (!slimelets.Contains(slimelett))
            {
                slimelets.Add(slimelett);
            }
        }
    }

    private void FixedUpdate()
    {
        for(int i = slimelets.Count - 1; i >= 0; i--)
        {
            Slimelet slimelet = slimelets[i];

            if(slimelet != null)
            {
                float size = slimelet.slimeletSizer.slimeSize;
                float distance = Vector2.Distance(transform.position, slimelet.transform.position);
                float appliedForce = fanForce / (1.0f + distance * distance);
                slimelet.rigidbody.AddForce(Vector2.up * appliedForce);
                //slimelet.rigidbody.AddForce(new Vector2(0, (fanMaxDistance - distance) * (fanForce - size * 0.3f)));
            }
            else
            {
                slimelets.RemoveAt(i);
            }
        }
    }
}
