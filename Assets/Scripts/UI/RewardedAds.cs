using UnityEngine;
using YG;

public class RewardedAds : MonoBehaviour
{
    private void Awake()
    {
        YandexGame.RewardVideoEvent += (id) => { Debug.Log(id); };
    }

    public void CallRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
}
