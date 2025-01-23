using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BabiSpawn : MonoBehaviour
{
    [SerializeField] private GameObject slimelettPrefab;
    private Vector3 baseScale = Vector3.one;
    private Vector3 scaleIncrement = new Vector3(0.25f, 0.25f, 0.25f);

    private float slimelettSpawnCooldownTimer = 0f;
    private float spawnSlimelettCooldown = 0.25f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanSpawnSlimelett())
        {
            SpawnSlimelett();
        }

        slimelettSpawnCooldownTimer += Time.deltaTime;
    }

    void SpawnSlimelett()
    {
        GameObject slimelettObj = Instantiate(slimelettPrefab, transform.position, Quaternion.identity);
        var slimelett = slimelettObj.GetComponent<Slimelett>();
        slimelett.walkDirection = Vector3.right;
        transform.localScale -= scaleIncrement;
        slimelett.transform.localScale = scaleIncrement;
        slimelettSpawnCooldownTimer = 0f;
    }

    bool CanSpawnSlimelett()
    {
        return transform.localScale != scaleIncrement && slimelettSpawnCooldownTimer >= spawnSlimelettCooldown;
    }
}
