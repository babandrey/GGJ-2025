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
        _startPos = transform.localPosition;
        _activatedPos = _startPos - transform.up * transform.localScale.y;
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
