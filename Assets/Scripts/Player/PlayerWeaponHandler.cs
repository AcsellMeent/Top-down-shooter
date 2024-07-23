using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlayerInputProvider))]
public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField]
    private Transform _weaponAxis;

    private ProjectileWeapon _weaponPrefab;
    private ProjectileWeapon _weapon;
    public ProjectileWeapon WeaponPrefab => _weaponPrefab;

    private PlayerInputProvider _playerInputProvider;

    private PauseManager _pauseManager;

    [Inject]
    public void Construct(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;
    }

    private void Awake()
    {
        _playerInputProvider = GetComponent<PlayerInputProvider>();
    }

    private void Update()
    {
        if (_pauseManager.IsPaused) return;

        if (_playerInputProvider.Input.PlayerControls.Shoot.ReadValue<float>() == 1)
        {
            _weapon?.PerformShoot();
        }
    }

    public void SetWeapon(ProjectileWeapon weapon)
    {
        if (_weapon) { Destroy(_weapon.gameObject); }
        _weaponPrefab = weapon;
        _weapon = Instantiate(weapon, _weaponAxis);
        _weapon.Initialize(_pauseManager);
    }
}
