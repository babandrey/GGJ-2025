using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _activatedPos;
    public float movingTime = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _activatedPos = new Vector3(_startPos.x, _startPos.y - transform.localScale.y, _startPos.z);
    }
    
    public void Activate()
    {
        transform.LeanMove(_activatedPos, movingTime).setEaseInOutSine();
    }

    public void Deactivate()
    {
        transform.LeanMove(_startPos, movingTime).setEaseInOutSine();
    }
}
