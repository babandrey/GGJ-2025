using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BabiSpawn : MonoBehaviour
{
    [SerializeField] private GameObject slimelettPrefab;
    private Vector3 baseScale = Vector3.one;
    private Vector3 scaleIncrement = new Vector3(0.25f, 0.25f, 0.25f);
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && transform.localScale != scaleIncrement)
        {
            SpawnSlimelett();
        }
    }

    void SpawnSlimelett()
    {
        GameObject slimelettObj = Instantiate(slimelettPrefab, transform.position, Quaternion.identity);
        var slimelett = slimelettObj.GetComponent<Slimelett>();
        slimelett.walkDirection = Vector3.right;
        transform.localScale -= scaleIncrement;
        slimelett.transform.localScale = scaleIncrement;
    }
}
