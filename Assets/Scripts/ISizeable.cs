using UnityEngine;

public interface ISizeable
{
    int slimeSize { get; set; }
    Vector3 currScale { get; set; }
    
    void Resize(int resizeAmount);
    void Grow(int amount);
    void Shrink(int amount);
    void SetScale();
}
