using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlayerRotateTowardsPointer))]
public class PlayerRotateTowardsPointer : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private Transform _playerViewTransform;

    private PlayerInputProvider _playerInputProvider;

    [SerializeField]
    private float _rotationSpeed;

    private Vector3 _targetDirection;

    private PauseManager _pauseManager;

    [Inject]
    public void Construct(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;
    }

    private void Awake()
    {
        _camera = Camera.main;
        _playerInputProvider = GetComponent<PlayerInputProvider>();
    }


    private void Update()
    {
        if (_pauseManager.IsPaused) return;
        
        if (_playerInputProvider.Input.PlayerControls.Shoot.ReadValue<float>() == 1)
        {
            _targetDirection = _camera.ScreenToWorldPoint(_playerInputProvider.Input.PlayerControls.MousePosition.ReadValue<Vector2>()) - transform.position;
            _targetDirection.z = transform.position.z;
            _targetDirection = _targetDirection.normalized;

            float angle = (Mathf.Atan2(_targetDirection.y, _targetDirection.x) - Mathf.PI / 2) * Mathf.Rad2Deg;
            angle = angle < 0 ? angle + 360 : angle;

            _playerViewTransform.rotation = Quaternion.RotateTowards(_playerViewTransform.rotation, Quaternion.Euler(0, 0, angle), 180 * Time.deltaTime);
        }
    }

}
