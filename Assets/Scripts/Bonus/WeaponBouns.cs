using UnityEngine;

public class WeaponBouns : Bonus
{
    [SerializeField]
    private ProjectileWeapon _weapon;

    public ProjectileWeapon Weapon => _weapon;

    protected override void ApplyBonus(PlayerBonusHandler bonusHandler)
    {
        bonusHandler.GiveWeapon(_weapon);
        Destroy(gameObject);
    }
}
