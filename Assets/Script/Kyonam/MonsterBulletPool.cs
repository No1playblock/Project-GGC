using UnityEngine;

public class MonsterBulletPool : ObjectPool<MonsterBulletPool, MonsterBullet>
{
    #region Components
    [SerializeField]
    private MonsterBullet bulletSource = null;
    #endregion

    private void Start()
    {
        InitPool(this, bulletSource, "Bullet", 300);
    }
}
