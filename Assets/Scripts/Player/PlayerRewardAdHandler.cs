using UnityEngine;
using YG;

[RequireComponent(typeof(PlayerEffectsHandler))]
public class PlayerRewardAdHandler : MonoBehaviour
{
    private PlayerEffectsHandler _playerEffectsHandler;

    private void Awake()
    {
        _playerEffectsHandler = GetComponent<PlayerEffectsHandler>();

        YandexGame.RewardVideoEvent += OnRewardAd;
    }

    private void OnRewardAd(int id)
    {
        switch (id)
        {
            case 0:
                _playerEffectsHandler.AddEffect<InvulnerabilityEffect>(10);
                break;
            case 1:
                _playerEffectsHandler.AddEffect<SpeedEffect>(10);
                break;
        }
    }

}
