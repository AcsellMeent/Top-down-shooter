using System;
using UnityEngine;
using YG;

public class RewardedAds : MonoBehaviour
{
    public void CallRewardAd(int id)
    {
        try
        {
            YandexGame.RewVideoShow(id);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            Destroy(gameObject);
        }
    }
}
