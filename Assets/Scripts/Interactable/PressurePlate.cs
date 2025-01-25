using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public int requiredSlimeWeight = 3;
    public Barrier affectedBarrier;
    private List<ISizeable> _slimelets = new List<ISizeable>();
    private bool _isPressed = false;
    private Animator _animatorController;
    
    private void Start()
    {
        _animatorController = gameObject.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Slimelett"))
        {
            ISizeable slime = other.GetComponent<ISizeable>();
            if (!_slimelets.Contains(slime))
                _slimelets.Add(slime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ISizeable slime))
        {
            _slimelets.Remove(slime);
        }
    }

    private void Update()
    {
        int currentWeight = 0;

        foreach (ISizeable slimelet in _slimelets)
            currentWeight += slimelet.slimeSize;
        
        if(!_isPressed && currentWeight >= requiredSlimeWeight)
            Pressed();
        if (_isPressed && currentWeight < requiredSlimeWeight)
            LetGo();
    }

    private void Pressed()
    {
        _isPressed = true;
        _animatorController.SetTrigger("pressed");
        AudioManager.Instance.PlaySound("PlatePressed");
        affectedBarrier.Activate();
    }

    private void LetGo()
    {
        _isPressed = false;
        _animatorController.SetTrigger("released");
        AudioManager.Instance.PlaySound("PlateReleased");
        affectedBarrier.Deactivate();
    }
}
