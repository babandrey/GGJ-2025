public interface ISizeable
{
    int slimeSize { get; set; }
    
    void Resize(int resizeAmount);
    void Grow(int amount);
    void Shrink(int amount);
}
