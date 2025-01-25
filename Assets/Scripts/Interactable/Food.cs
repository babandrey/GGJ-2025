using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    public int growAmount;
    public float spinSpeed;
    public float randomRange;
    private float rotationSign;
    private void Start()
    {
        rotationSign = Random.Range(0, 2) == 0 ? -1 : 1;
        transform.rotation = Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f));
        spinSpeed = Random.Range(spinSpeed - randomRange, spinSpeed + randomRange);
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSign) * Time.deltaTime * spinSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Slimelett"))
        {
            collision.gameObject.GetComponent<ISizeable>().Resize(growAmount);
            transform.LeanScale(Vector3.zero, 0.25f).setDestroyOnComplete(true);
        }
    }
}
