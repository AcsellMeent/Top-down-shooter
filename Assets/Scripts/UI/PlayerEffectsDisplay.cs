using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerEffectsDisplay : MonoBehaviour
{
    [SerializeField]
    private EffectDisplayItem[] _effectDisplayItem = new EffectDisplayItem[2];

    private PlayerEffectsHandler _playerEffectsHandler;

    private Dictionary<Type, EffectDisplayItem> _effectItemDictionary;

    [SerializeField]
    private float _countdownStep;


    [Inject]
    public void Construct(PlayerEffectsHandler playerEffectsHandler)
    {
        _playerEffectsHandler = playerEffectsHandler;
        _playerEffectsHandler.OnEffectApplied += OnEffectApplied;

        _effectItemDictionary = new Dictionary<Type, EffectDisplayItem>
        {
            {typeof(InvulnerabilityEffect), _effectDisplayItem[0]},
            {typeof(SpeedEffect), _effectDisplayItem[1]}
        };

        Debug.Log($"{_effectItemDictionary.Keys.Count}");
    }

    private void OnDestroy()
    {
        _playerEffectsHandler.OnEffectApplied -= OnEffectApplied;
    }

    private void OnEffectApplied(Type type, float duration)
    {
        Debug.Log($"Cont {_effectItemDictionary.ContainsKey(type)}");

        EffectDisplayItem item = _effectItemDictionary[type];
        if (item.Coroutine != null) { StopCoroutine(item.Coroutine); }
        item.Coroutine = StartCoroutine(EffectCountdown(item, duration));
        Debug.Log(item);
    }

    private IEnumerator EffectCountdown(EffectDisplayItem item, float duration)
    {
        item.RemainingTime = duration;
        item.Image.gameObject.SetActive(true);
        while (item.RemainingTime > 0)
        {
            yield return new WaitForSeconds(_countdownStep);
            item.RemainingTime -= _countdownStep;
            item.Image.fillAmount = item.RemainingTime / duration;
            item.Counter.text = ((int)item.RemainingTime).ToString();
        }
        item.Image.gameObject.SetActive(false);
        item.Coroutine = null;
    }

}

[Serializable]
class EffectDisplayItem
{
    public Image Image;
    public TMP_Text Counter;
    public float RemainingTime;
    public Coroutine Coroutine;
}