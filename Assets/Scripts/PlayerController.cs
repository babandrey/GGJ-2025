using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float jumpForce;
    public float sizeJumpModifier;
    public float friction;

    private float currentSpeed;
    public float maxSpeed;

    public LayerMask ground;

    [HideInInspector] public Rigidbody2D rb;
    private CircleCollider2D cCollider;
    [HideInInspector] public BabiSizer babiSizer;
    private bool isDead = false;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cCollider = GetComponent<CircleCollider2D>();
        babiSizer = GetComponent<BabiSizer>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;

        float move = Input.GetAxisRaw("Horizontal");
        if(move != 0 )
        {
            currentSpeed += acceleration * move * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, friction * Time.deltaTime);
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }


        if (Input.GetButtonDown("Jump") && CheckIsGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce - babiSizer.slimeSize * sizeJumpModifier),
                ForceMode2D.Impulse);
            _animator.SetTrigger("jumped");
        }

    }

    bool CheckIsGrounded()
    {
        return Physics2D.Raycast(transform.position + Vector3.up * cCollider.radius, Vector2.down, cCollider.radius + 0.01f, ground.value);
    }

    public void Kill()
    {
        isDead = true;
        transform.LeanScale(Vector3.zero, 0.35f).setEaseInOutSine(); // kill animation
        AudioManager.Instance.PlaySound("LevelLose");
        LevelManager.Instance.RestartLevel(0.8f);
    }
}
