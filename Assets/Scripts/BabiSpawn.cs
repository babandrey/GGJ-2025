using UnityEngine;

public class BabiSpawn : MonoBehaviour
{
    [SerializeField] private Slimelet slimeletPrefab;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private BabiSizer babiSizer;

    private float slimeletSpawnCooldownTimer = 0f;
    private float spawnSlimeletCooldown = 0.25f;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanSpawnSlimelet())
            SpawnSlimelet();
        
        if(slimeletSpawnCooldownTimer > 0)
            slimeletSpawnCooldownTimer -= Time.deltaTime;
    }

    void SpawnSlimelet()
    {
        Slimelet slimelet = Instantiate(slimeletPrefab);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 babiToCursorDirection = (mousePos - transform.position).normalized;
        var spawnDirection = babiToCursorDirection.x > 0 ? Vector3.right : Vector3.left;
        
        slimelet.walkDirection = spawnDirection;
        slimelet.transform.position =
            new Vector3(transform.position.x + circleCollider.radius * spawnDirection.x * 0.5f, transform.position.y);
        slimelet.transform.localScale = Vector3.zero;
        slimelet.slimeletSizer.Resize(1);
        
        babiSizer.Resize(-1);
        slimeletSpawnCooldownTimer = spawnSlimeletCooldown;
    }

    bool CanSpawnSlimelet()
    {
        return babiSizer.IsShrinkable() && slimeletSpawnCooldownTimer <= 0;
    }
}
