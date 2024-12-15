using System;

public interface IReadOnlyReactiveProperty<T>
{
    event Action<T> OnValueChanged;
    
    public T Value { get; }
}