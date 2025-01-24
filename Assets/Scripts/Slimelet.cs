using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Slimelet : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    [SerializeField] private float speed;
    [SerializeField] private float stopFriction;
    [HideInInspector] public Vector3 walkDirection = Vector3.zero;
    private bool isStopped = false;
    [HideInInspector] public float lifeTime = 0.0f;
    public SlimeletSizer slimeletSizer;
    
    void FixedUpdate()
    {
        lifeTime += Time.fixedDeltaTime;

        if (!isStopped)
        {
            Vector2 velocity = walkDirection * Time.fixedDeltaTime * speed;
            rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y);
        }
        else
        {
            float xVelocity = Mathf.Max(0, rigidbody.velocity.x * stopFriction);
            rigidbody.velocity = new Vector2(xVelocity, rigidbody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AnchorPoint"))
        {
            isStopped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // moving slimelett should be the one to merge into stationary slimelett
        if (collision.gameObject.TryGetComponent(out Slimelet slimelett) && this.lifeTime < slimelett.lifeTime)
        {
            MergeToSlimelett(slimelett);
        }
    }

    void MergeToSlimelett(Slimelet other)
    {
        this.rigidbody.simulated = false;
        this.transform.LeanMove(other.transform.position, 0.25f).setEaseInCubic().setDestroyOnComplete(true);
        other.slimeletSizer.Resize(this.slimeletSizer.slimeSize);
    }
}
