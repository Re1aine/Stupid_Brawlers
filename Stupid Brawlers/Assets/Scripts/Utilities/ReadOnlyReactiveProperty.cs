using System;

public class ReadOnlyReactiveProperty<T> : IReadOnlyReactiveProperty<T>, IDisposable
{
    public event Action<T> OnValueChanged;
    
    public T Value => _source.Value;

    private readonly ReactiveProperty<T> _source;
    
    public ReadOnlyReactiveProperty(ReactiveProperty<T> source)
    {
        _source = source;
        _source.OnValueChanged += NotifyValueChanged;
    }

    private void NotifyValueChanged(T value) => OnValueChanged?.Invoke(Value);


    public void Dispose() => _source.OnValueChanged -= NotifyValueChanged;
}