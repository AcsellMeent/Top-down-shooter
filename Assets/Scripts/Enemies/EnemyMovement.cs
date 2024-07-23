using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Transform _playerTransform;

    private Rigidbody2D _rigidbody;

    private PauseManager _pauseManager;

    public void Init(Transform playerTransform, PauseManager pauseManager)
    {
        _playerTransform = playerTransform;
        _pauseManager = pauseManager;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Мог бы ещё остановить аниматор во время паузы, но мне нравится что кадр не замирает полностью
        if (_pauseManager.IsPaused) return;
        Vector2 direction = _playerTransform.position - transform.position;
        _rigidbody.MovePosition(_rigidbody.position + direction.normalized * _speed * Time.deltaTime);
    }
}
