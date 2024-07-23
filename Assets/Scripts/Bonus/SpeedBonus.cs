public class SpeedBonus : Bonus
{
    protected override void ApplyBonus(PlayerBonusHandler bonusHandler)
    {
        bonusHandler.EffectsHandler.AddEffect<SpeedEffect>(10);
        Destroy(gameObject);
    }
}
