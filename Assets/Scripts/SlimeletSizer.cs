using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeletSizer : MonoBehaviour, ISizeable
{
    [field: SerializeField] public int slimeSize { get; set; }
    public Vector3 currScale { get; set; }
    private readonly Vector3 _scaleIncrement = new Vector3(0.25f, 0.25f, 0.25f);

    private void Start()
    {
        currScale = _scaleIncrement;
    }

    public void Resize(int resizeAmount)
    {
        if (resizeAmount > 0)
            Grow(resizeAmount);
        if (resizeAmount < 0 && IsShrinkable())
            Shrink(-resizeAmount);
    }

    public void Grow(int amount)
    {
        slimeSize += amount;
        currScale += (_scaleIncrement * amount);
        SetScale();
    }

    public void Shrink(int amount)
    {
        if (amount > slimeSize)
            amount = slimeSize - 1;
        
        slimeSize -= amount;
        currScale -= (_scaleIncrement * amount);
        SetScale();
    }

    public void SetScale()
    {
        LeanTween.cancel(gameObject);
        transform.LeanScale(currScale, 0.25f).setEaseInOutSine();
    }

    public bool IsShrinkable()
    {
        return slimeSize > 0;
    }
}
