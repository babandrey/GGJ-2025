using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabiSizer : MonoBehaviour, ISizeable
{
    [field: SerializeField] public int slimeSize { get; set; }
    private readonly Vector3 _scaleIncrement = new Vector3(0.25f, 0.25f, 0.25f);
    
    public void Resize(int resizeAmount)
    {
        if (resizeAmount > 0)
            Grow(resizeAmount);
        if (resizeAmount < 0 && IsShrinkable())
            Shrink(-resizeAmount);
    }

    public void Grow(int amount)
    {
        transform.LeanScale(transform.localScale + (_scaleIncrement * amount), 0.05f).setEaseOutBounce();
        slimeSize += amount;
    }

    public void Shrink(int amount)
    {
        if (amount > slimeSize)
            amount = slimeSize - 1;
        
        transform.LeanScale(transform.localScale - (_scaleIncrement * amount), 0.25f).setEaseInOutSine();
        slimeSize -= amount;
    }

    public bool IsShrinkable()
    {
        return slimeSize > 1;
    }
}
