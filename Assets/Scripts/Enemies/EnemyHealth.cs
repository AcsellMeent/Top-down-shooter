using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _maxHealth;

    private float _health;

    [SerializeField]
    private Image _healthBar;

    [SerializeField]
    private Animator _animator;

    public event Action<int> OnDeath;
    
    [SerializeField]
    private int _points;

    private void Awake()
    {
        _health = _maxHealth;
        if (_animator == null) throw new Exception("_animator is null");
    }

    public void ApplyDammage(float damage)
    {
        if (damage < 0) throw new ArgumentOutOfRangeException("damage < 0");
        _health -= damage;
        _animator.SetTrigger("Damage");
        if (_health <= 0)
        {
            OnDeath?.Invoke(_points);
            Destroy(gameObject);
        }
        _healthBar.fillAmount = _health / _maxHealth;
    }
}
