using UnityEngine;

[RequireComponent(typeof(PlayerEffectsHandler))]
public class PlayerBonusHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerWeaponHandler _weaponHandler;

    [SerializeField]
    private PlayerEffectsHandler _effectsHandler;

    public PlayerEffectsHandler EffectsHandler => _effectsHandler;

    private void Awake()
    {
        _weaponHandler = GetComponent<PlayerWeaponHandler>();
        _effectsHandler = GetComponent<PlayerEffectsHandler>();
    }

    public void GiveWeapon(ProjectileWeapon weapon)
    {
        _weaponHandler.SetWeapon(weapon);
    }
}
