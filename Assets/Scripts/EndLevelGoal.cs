using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelGoal : MonoBehaviour
{
    [HideInInspector] public static int currentGoals;
    [HideInInspector] public static int goalsRequired;

    private bool isCovered = false;
    private GameObject currentCoverObject;

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

            //TODO: Add visuals
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
        }

    }
}
