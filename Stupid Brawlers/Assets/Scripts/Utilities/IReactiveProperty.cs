public interface IReactiveProperty<T>
{
    T Value { get; }
    
    void SetValue(T value);
}