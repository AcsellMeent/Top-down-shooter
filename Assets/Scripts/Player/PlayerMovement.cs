using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInputProvider))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private PlayerInputProvider _playerInputProvider;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _speedMultiplier;

    private PauseManager _pauseManager;

    [Inject]
    public void Construct(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInputProvider = GetComponent<PlayerInputProvider>();
        _speedMultiplier = 1;
    }

    private void FixedUpdate()
    {
        if (_pauseManager.IsPaused) return;
        
        Vector2 direction = _playerInputProvider.Input.PlayerControls.Movement.ReadValue<Vector2>();
        _rigidbody.MovePosition(_rigidbody.position + direction * _speed * _speedMultiplier * Time.deltaTime);
    }

    public void AddSpeedMultiplier(float multiplier)
    {
        if (multiplier < 0) throw new ArgumentOutOfRangeException("multiplier < 0");
        _speedMultiplier *= multiplier;
    }
    public void RemoveSpeedMultiplier(float multiplier)
    {
        if (multiplier < 0) throw new ArgumentOutOfRangeException("multiplier < 0");
        _speedMultiplier /= multiplier;
    }
}
