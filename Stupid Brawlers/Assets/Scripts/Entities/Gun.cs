public class Gun
{
    private readonly GunView _view;

    public Gun(GunView view, int bulletCount)
    {
        _view = view;
        _bulletCount = bulletCount;
    }

    private int _bulletCount;

    public void SetBulletCount(int bulletCount) => _bulletCount = bulletCount;

    public int GetBulletCount() => _bulletCount;
}