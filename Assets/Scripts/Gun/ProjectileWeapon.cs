using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField]
    protected Transform _muzzle;

    [SerializeField]
    protected Projectile _projectile;

    [Space(10)]
    [Header("Config")]

    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _fireRate;

    [SerializeField]
    private float _projectileSpeed;

    [Space(10)]
    [Header("Spread")]

    [SerializeField]
    private bool _spread;

    [ShowIf("_spread")]
    [SerializeField]
    [Min(0)]
    private float _spreadRange;

    [ShowIf("_spread")]
    [SerializeField]
    private int _projectileCount;

    [ShowIf("_spread")]
    [SerializeField]
    private bool _limitDistance;

    [ShowIf(EConditionOperator.And, "_limitDistance", "_spread")]
    [SerializeField]
    [Min(0)]
    private float _distance;

    private bool _showPointer => !_spread;

    [Space(10)]

    [ShowIf("_showPointer")]
    [SerializeField]
    private bool _pointer;

    private Camera _mainCamera;

    private InputActions _input;

    private bool _reloading;

    private PauseManager _pauseManager;

    private void Awake()
    {
        if (_pointer)
        {
            _mainCamera = Camera.main;
            _input = new InputActions();
        }
    }

    public void Initialize(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;
    }

    public void PerformShoot()
    {
        if (_projectile == null || _reloading) return;
        if (_spread)
        {
            float spreadStep = _spreadRange / _projectileCount;
            float baseAngle = _muzzle.rotation.eulerAngles.y + _spreadRange / 2;
            for (int i = 0; i < _projectileCount; i++)
            {
                float angle = baseAngle - spreadStep * i - (spreadStep / 2);
                Quaternion rotation = _muzzle.rotation * Quaternion.Euler(0, 0, angle);
                InstantiateProjectile(rotation);
            }
        }
        else
        {
            InstantiateProjectile(_muzzle.rotation);
        }
        StartCoroutine(Reload());
    }

    protected void InstantiateProjectile(Quaternion rotation)
    {
        Projectile projectile = Instantiate(_projectile, _muzzle.position, rotation);
        if (_pointer)
        {
            Vector3 point = _mainCamera.ScreenToWorldPoint(_input.PlayerControls.MousePosition.ReadValue<Vector2>());
            point.z = 0;
            projectile.Initialize(_pauseManager, _damage, _projectileSpeed, point);
            return;
        }
        if (_limitDistance)
        {
            projectile.Initialize(_pauseManager, _damage, _projectileSpeed, _distance);
            return;
        }
        projectile.Initialize(_pauseManager, _damage, _projectileSpeed);
    }

    private IEnumerator Reload()
    {
        _reloading = true;
        yield return new WaitForSeconds(1 / _fireRate);
        _reloading = false;
    }

    private void OnValidate()
    {
        _limitDistance = _pointer || !_spread ? false : _limitDistance;
        _pointer = _spread ? false : _pointer;
    }

    private void OnEnable()
    {
        _input?.Enable();
    }

    private void OnDisable()
    {
        _input?.Disable();
    }
}
