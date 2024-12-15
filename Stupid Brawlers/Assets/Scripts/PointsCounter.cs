
public class PointsCounter
{ 
    private int _receivedPoints;
    
    public void AddPoints(int value) => _receivedPoints += value;

    public int GetReceivedPoints() => _receivedPoints;
}