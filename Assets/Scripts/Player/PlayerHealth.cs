using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnDeath;

    private bool _invulnerability;

    private PauseManager _pauseManager;

    [Inject]
    public void Construct(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;
    }

    public void ApplyDammage()
    {
        if (_pauseManager.IsPaused) return;
        if (_invulnerability) return;
        OnDeath?.Invoke();
    }

    public void SetInvulnerability(bool status)
    {
        _invulnerability = status;
    }

    public void Kill()
    {
        OnDeath?.Invoke();
    }
}
