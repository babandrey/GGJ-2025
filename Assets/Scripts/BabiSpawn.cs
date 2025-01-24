using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BabiSpawn : MonoBehaviour
{
    [SerializeField] private GameObject slimelettPrefab;
    [SerializeField] private CircleCollider2D circleCollider;

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
        GameObject slimelettObj = Instantiate(slimelettPrefab);
        var slimelett = slimelettObj.GetComponent<Slimelett>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 babiToCursorDirection = (mousePos - transform.position).normalized;
        var spawnDirection = babiToCursorDirection.x > 0 ? Vector3.right : Vector3.left;
        slimelett.walkDirection = spawnDirection;
        slimelett.transform.position = new Vector3(transform.position.x + circleCollider.radius * spawnDirection.x * 0.5f, transform.position.y);
        transform.LeanScale(transform.localScale - scaleIncrement, 0.25f).setEaseInOutSine();
        slimelett.transform.LeanScale(scaleIncrement, 0.05f).setEaseOutBounce().setFrom(Vector3.zero);
        slimelettSpawnCooldownTimer = 0f;
    }

    bool CanSpawnSlimelett()
    {
        return transform.localScale != scaleIncrement && slimelettSpawnCooldownTimer >= spawnSlimelettCooldown;
    }
}
