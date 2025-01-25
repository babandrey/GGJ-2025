using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelGoal : MonoBehaviour
{
    [HideInInspector] public static int currentGoals;
    [HideInInspector] public static int goalsRequired;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isCovered = false;
    private GameObject currentCoverObject;

    public float sineAmp;
    public float sineFreq;
    public Color coveredColor;
    public float colorLerp;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        rb.velocity = Vector2.up * sineAmp * Mathf.Sin(Time.time * sineFreq);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCovered)
        {
            currentGoals++;
            isCovered = true;
            currentCoverObject = collision.gameObject;

            if (currentGoals >= goalsRequired)
            {
                print("we won!!");
                LevelManager.Instance.GoNextLevel();
            }

            LeanTween.color(gameObject, coveredColor, 0.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isCovered && currentCoverObject == collision.gameObject)
        {
            currentGoals--;
            isCovered = false;
            currentCoverObject = null;
            // TODO: Add visuals
            LeanTween.color(gameObject, Color.white, 0.3f);
        }

    }
}
