using System.Collections;
using UnityEngine;

public abstract class Effect
{
    public abstract void Apply();
    public abstract void Remove();
    public float Duration { get; protected set; }
    protected PlayerEffectsHandler EffectsHandler;

    protected Effect(float duration, PlayerEffectsHandler effectsHandler)
    {
        Duration = duration;
        EffectsHandler = effectsHandler;
    }
}
