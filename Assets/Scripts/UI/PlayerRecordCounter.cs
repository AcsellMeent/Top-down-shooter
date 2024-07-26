using UnityEngine;
using TMPro;
using Zenject;
using System;
using YG;

public class PlayerRecordCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;

    [SerializeField]
    private string _labelFormat;

    private int _points;

    public event Action<bool, int> OnResult;

    [SerializeField]
    private string _leaderBoardName;

    [Inject]
    public void Construct(EnemySpawner enemySpawner, PlayerHealth playerHealth)
    {
        enemySpawner.OnEnemyDeath += OnEnemyDeath;
        playerHealth.OnDeath += OnPlayerDeath;
    }

    private void Awake()
    {
        UpdateLabel();
    }

    private void OnEnemyDeath(int points)
    {
        _points += points;
        UpdateLabel();
    }

    private void OnPlayerDeath()
    {
        PlayerRecord playerRecord = StorageSystem.LoadPlayerRecord();
        if (playerRecord == null || _points > playerRecord.Points)
        {
            OnResult?.Invoke(true, _points);
            playerRecord = new PlayerRecord(_points);
            StorageSystem.SavePlayerRecord(playerRecord);
            YandexGame.NewLeaderboardScores(_leaderBoardName, _points);
            return;
        }
        OnResult?.Invoke(false, _points);
    }

    private void UpdateLabel()
    {
        _label.text = string.Format(_labelFormat, _points);
    }

}
