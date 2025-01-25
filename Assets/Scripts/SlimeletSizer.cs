using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeletSizer : MonoBehaviour, ISizeable
{
    [field: SerializeField] public int slimeSize { get; set; }
    private readonly Vector3 _scaleIncrement = new Vector3(0.25f, 0.25f, 0.25f);


    public void Resize(int resizeAmount)
    {
        if (resizeAmount > 0)
        {
            Grow(resizeAmount);
            AudioManager.Instance.PlaySound("Merge");
        }
        
        if (resizeAmount < 0 && IsShrinkable())
        {
            Shrink(-resizeAmount);
            AudioManager.Instance.PlaySound("Merge");
        }
    }

    public void Grow(int amount)
    {
        slimeSize += amount;
        SetScale();
    }

    public void Shrink(int amount)
    {
        if (amount > slimeSize)
            amount = slimeSize - 1;
        
        slimeSize -= amount;
        SetScale();
    }

    public void SetScale()
    {
        Vector3 targetScale = _scaleIncrement * slimeSize;
        LeanTween.cancel(gameObject);
        transform.LeanScale(targetScale, 0.25f).setEaseInOutSine();
    }

    public bool IsShrinkable()
    {
        return slimeSize > 0;
    }
}
