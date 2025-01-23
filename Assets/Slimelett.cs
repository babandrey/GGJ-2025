using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimelett : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private float speed = 150.0f;
    public Vector3 walkDirection = Vector3.zero;
    void FixedUpdate()
    {
        Vector3 velocity = walkDirection * Time.deltaTime * speed;
        rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.z);
    }
}
