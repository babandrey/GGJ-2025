using System;
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
    [SerializeField] private CircleCollider2D triggerCollider;
    
    private void Start()
    {
        StartCoroutine(WaitToActivateBabiTrigger());
    }

    private IEnumerator WaitToActivateBabiTrigger()
    {
        yield return new WaitForSeconds(.5f);
        triggerCollider.enabled = true;
    }
    
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

        // moving slimelett should be the one to merge into stationary slimelett
        if (collision.gameObject.TryGetComponent(out PlayerController babi))
        {
            MergeToBabi(babi);
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // moving slimelett should be the one to merge into stationary slimelett
        if (collision.gameObject.TryGetComponent(out Slimelet slimelet) && this.lifeTime < slimelet.lifeTime)
            MergeToSlimelet(slimelet);
    }

    void MergeToBabi(PlayerController babi)
    {
        this.rigidbody.simulated = false;
        int size = this.slimeletSizer.slimeSize;
        this.slimeletSizer.Resize(-size);
        this.transform.LeanMove(babi.transform.position, 0.25f).setEaseInCubic().setDestroyOnComplete(true);
        babi.GetComponent<BabiSizer>().Resize(size);
    }
    
    void MergeToSlimelet(Slimelet other)
    {
        this.rigidbody.simulated = false;
        int size = this.slimeletSizer.slimeSize;
        this.slimeletSizer.Resize(-size);
        this.transform.LeanMove(other.transform.position, 0.25f).setEaseInCubic().setDestroyOnComplete(true);
        other.slimeletSizer.Resize(size);
    }
}
