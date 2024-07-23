using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameoverWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject _windowView;

    [SerializeField]
    private GameObject _newRecordTitle;

    [SerializeField]
    private TMP_Text _pointsLabel;

    [SerializeField]
    private string _pointsLabelFormat;

    private PauseManager _pauseManager;

    [Inject]
    public void Construct(PlayerHealth playerHealth, PauseManager pauseManager, PlayerRecordCounter playerRecordCounter)
    {
        _pauseManager = pauseManager;
        playerHealth.OnDeath += OnPlayerDeath;
        playerRecordCounter.OnResult += OnResult;
    }

    private void OnPlayerDeath()
    {
        _pauseManager.SetPaused(true);
        _windowView.SetActive(true);
    }

    private void OnResult(bool isNewRecord, int points)
    {
        if (points != 0)
        {
            _newRecordTitle.SetActive(isNewRecord);
        }
        _pointsLabel.text = string.Format(_pointsLabelFormat, points.ToString());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
