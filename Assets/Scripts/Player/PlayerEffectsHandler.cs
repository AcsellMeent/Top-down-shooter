using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerWeaponHandler))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerEffectsHandler : MonoBehaviour
{
    public event Action<Type, float> OnEffectApplied;
    private List<Effect> _effects = new List<Effect>();
    private Dictionary<Type, EffectDurationConfig> _effectDurationConfig = new Dictionary<Type, EffectDurationConfig>();

    public void AddEffect<T>(float duration) where T : Effect
    {
        Effect currentEffect = _effects.FirstOrDefault(e => e is T);
        if (currentEffect != null)
        {
            EffectDurationConfig config = _effectDurationConfig[typeof(T)];
            if (duration > (Time.time - config.StartTime))
            {
                StopCoroutine(config.Coroutine);
                currentEffect.Remove();
                _effects.Remove(currentEffect);
                _effectDurationConfig.Remove(typeof(T));

                CreateEffect<T>(duration);
            }
        }
        else
        {
            CreateEffect<T>(duration);
        }
        OnEffectApplied?.Invoke(typeof(T), duration);
    }

    private void CreateEffect<T>(float duration) where T : Effect
    {
        Debug.Log($"CREATE EFFECT {typeof(T)} {this}");
        Effect newEffect = (T)Activator.CreateInstance(typeof(T), duration, this);

        _effects.Add(newEffect);
        newEffect.Apply();

        Coroutine coroutine = StartCoroutine(RemoveAfterDuration<T>(newEffect, duration));
        _effectDurationConfig.Add(typeof(T), new EffectDurationConfig(Time.time, coroutine));
    }

    private IEnumerator RemoveAfterDuration<T>(Effect effect, float duration) where T : Effect
    {
        yield return new WaitForSeconds(duration);
        effect.Remove();
        _effects.Remove(effect);
        _effectDurationConfig.Remove(typeof(T));
    }
}

public class EffectDurationConfig
{
    public readonly float StartTime;
    public readonly Coroutine Coroutine;
    public EffectDurationConfig(float startTime, Coroutine coroutine)
    {
        StartTime = startTime;
        Coroutine = coroutine;
    }
}