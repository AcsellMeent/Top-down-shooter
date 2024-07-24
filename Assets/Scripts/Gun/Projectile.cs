using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IPauseHandler
{
    private Rigidbody2D _rigidbody;
    private float _damage;
    private float _speed;
    private float? _distance;
    private Vector3 _startPosition;
    private Vector3? _target;

    private PauseManager _pauseManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(PauseManager pauseManager, float damage, float speed)
    {
        _pauseManager = pauseManager;
        _pauseManager.Register(this);
        _damage = damage;
        _speed = speed;
    }

    public void Initialize(PauseManager pauseManager, float damage, float speed, float distance)
    {
        Initialize(pauseManager, damage, speed);
        _distance = distance;
        _startPosition = transform.position;
    }

    public void Initialize(PauseManager pauseManager, float damage, float speed, Vector3 target)
    {
        Initialize(pauseManager, damage, speed);
        _target = target;
        StartCoroutine(MoveTowards());
    }

    private void FixedUpdate()
    {
        if (_pauseManager.IsPaused)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        if (_target == null)
        {
            _rigidbody.velocity = transform.up * _speed;
            if (_distance != null && Vector3.Distance(_startPosition, transform.position) > _distance)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator MoveTowards()
    {
        while (transform.position != _target)
        {
            transform.position = Vector3.MoveTowards(transform.position, (Vector3)_target, _speed * Time.deltaTime);
            yield return null;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);
        for (int i = 0; i < colliders.Length; i++)
        {
            TryApplyDamage(colliders[i]);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_target != null) return;
        TryApplyDamage(other);
        if (other.isTrigger) return;
        Destroy(gameObject);
    }

    private void TryApplyDamage(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.ApplyDammage(_damage);
        }
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            StopAllCoroutines();
        }
    }

    private void OnDestroy()
    {
        _pauseManager.UnRegister(this);
    }
}
