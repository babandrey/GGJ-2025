using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimelett : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float speed = 150.0f;
    [HideInInspector] public Vector3 walkDirection = Vector3.zero;
    private bool isMoving = true;
    private bool isMerging = false;
    [HideInInspector] public float lifeTime = 0.0f;
    void FixedUpdate()
    {
        lifeTime += Time.fixedDeltaTime;

        if (!isMoving) return;

        Vector3 velocity = walkDirection * Time.deltaTime * speed;
        rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AnchorPoint"))
        {
            isMoving = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("hewo");
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
