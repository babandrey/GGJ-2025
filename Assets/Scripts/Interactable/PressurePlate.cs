using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public int requiredSlimeWeight = 3;
    public Barrier affectedBarrier;
    private List<Slimelet> _slimelets = new List<Slimelet>();
    private bool _isPressed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Slimelet slimelet))
        {
            _slimelets.Add(slimelet);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Slimelet slimelet))
        {
            _slimelets.Remove(slimelet);
        }
    }

    private void Update()
    {
        int currentWeight = 0;

        foreach (Slimelet slimelet in _slimelets)
            currentWeight += slimelet.slimeletSizer.slimeSize;
        
        if(!_isPressed && currentWeight >= requiredSlimeWeight)
            Pressed();
        if (_isPressed && currentWeight < requiredSlimeWeight)
            LetGo();
    }

    private void Pressed()
    {
        _isPressed = true;
        Debug.Log("Pressed");
        affectedBarrier.Activate();
    }

    private void LetGo()
    {
        _isPressed = false;
        Debug.Log("Let Go");
        affectedBarrier.Deactivate();
    }
}
