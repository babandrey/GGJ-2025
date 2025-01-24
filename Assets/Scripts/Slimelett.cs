using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimelett : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float speed;
    [SerializeField] private float stopFriction;
    [HideInInspector] public Vector3 walkDirection = Vector3.zero;
    private bool isStopped = false;
    private bool isMerging = false;
    [HideInInspector] public float lifeTime = 0.0f;
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
        if (collision.gameObject.TryGetComponent(out Slimelett slimelett) && this.lifeTime < slimelett.lifeTime)
        {
            MergeToSlimelett(slimelett);
        }
    }

    void MergeToSlimelett(Slimelett other)
    {
        this.rigidbody.simulated = false;
        this.transform.LeanMove(other.transform.position, 0.25f).setEaseInCubic().setDestroyOnComplete(true);
        other.transform.LeanScale(other.transform.localScale + this.transform.localScale, 0.25f).setEaseInCubic();
    }
}
