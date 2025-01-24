using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(UpdateGoalsRequired());
        }
    }

    public void GoNextLevel()
    {
        int buildIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(buildIndex);
        
        StartCoroutine(UpdateGoalsRequired());
    }

    // have to do this in coroutine otherwise OnTriggerExit calls in the new scene and causees incorrect count
    private IEnumerator UpdateGoalsRequired()
    {
        yield return null;
        var endLevelGoals = FindObjectsOfType<EndLevelGoal>();
        EndLevelGoal.goalsRequired = endLevelGoals.Length;
        EndLevelGoal.currentGoals = 0;
    }
}
