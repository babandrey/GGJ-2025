using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float move;
    public float speed;
    public float jumpForce = 10;

    public LayerMask ground;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move*speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && CheckIsGrounded())
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);

    }

    bool CheckIsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, gameObject.GetComponent<CircleCollider2D>().radius + 0.01f, ground.value);
    }
}
