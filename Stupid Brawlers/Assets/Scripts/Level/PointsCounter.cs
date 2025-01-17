public class PointsCounter
{
    public ReadOnlyReactiveProperty<int> ReceivePoints => new(_receivePoints);
    
    private readonly ReactiveProperty<int> _receivePoints = new();
    
    public void AddPoints(int value) => _receivePoints.SetValue(_receivePoints.Value + value);
}


