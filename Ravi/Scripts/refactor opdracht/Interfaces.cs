using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int _damage);
}

public interface IBullet
{
    void DestroyBullet();
    void SetEffects(GameObject _hitEffect, AudioClip _hitSfx);
}