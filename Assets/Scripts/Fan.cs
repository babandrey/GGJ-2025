using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    List<Slimelett> slimelets = new List<Slimelett>();
    [SerializeField] private float fanMaxDistance;
    [SerializeField] private float fanForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Slimelett slimelett))
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
            Slimelett slimelett = slimelets[i];

            if(slimelett != null)
            {
                float size = slimelett.transform.localScale.x * 4f; // small size == 1, medium == 2 etc
                float distance = Vector2.Distance(transform.position, slimelett.transform.position);
                slimelett.rigidbody.AddForce(new Vector2(0, (fanMaxDistance - distance) * (fanForce - size * 0.3f)));
            }
            else
            {
                slimelets.RemoveAt(i);
            }
        }
    }
}
