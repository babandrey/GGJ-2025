using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private CanvasGroup canvasGroup;
    public bool transitioning { get; private set; } = false;
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
            canvasGroup = GetComponentInChildren<CanvasGroup>();
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                AudioManager.Instance.PlayMusic();
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void GoNextLevel(float delay = 1f)
    {
        if (transitioning) return;

        int buildIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.Instance.PlayMusic();
        }
        else
        {
            AudioManager.Instance.PlaySound("LevelWin");
        }

        EndLevelGoal.sineMove = false;
        transitioning = true;
        canvasGroup.LeanAlpha(1f, 1f).setDelay(delay).setOnComplete(() =>
        {
            SceneManager.LoadScene(buildIndex);
            StartCoroutine(UpdateGoalsRequired());
            canvasGroup.LeanAlpha(0f, 1f);
            EndLevelGoal.sineMove = true;
            transitioning = false;
        });
    }

    public void RestartLevel(float delay = 0f)
    {
        transitioning = true;
        canvasGroup.LeanAlpha(1f, 1f).setDelay(delay).setOnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            StartCoroutine(UpdateGoalsRequired());
            canvasGroup.LeanAlpha(0f, 1f);
            transitioning = false;
        });
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