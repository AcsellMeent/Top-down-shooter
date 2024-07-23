public class SpeedEffect : Effect
{
    public SpeedEffect(float duration, PlayerEffectsHandler effectsHandler) : base(duration, effectsHandler) { }

    public override void Apply()
    {
        EffectsHandler.GetComponent<PlayerMovement>().AddSpeedMultiplier(1.5f);
    }
    public override void Remove()
    {
        EffectsHandler.GetComponent<PlayerMovement>().RemoveSpeedMultiplier(1.5f);
    }
}
