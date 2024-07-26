using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using YG;
using YG.Utils.LB;
using System;

public class LeaderBoardDisplay : MonoBehaviour
{
    [SerializeField]
    private string _leaderBoardName;

    [SerializeField]
    private RectTransform _recordPrefab;

    [SerializeField]
    private RectTransform _recordAxis;


    private void Awake()
    {
        YandexGame.GetLeaderboard(_leaderBoardName, 5, 3, 1, "");
        YandexGame.onGetLeaderboard += OnGetLeaderBoard;
    }

    private void OnGetLeaderBoard(LBData data)
    {
        for (int i = 0; i < data.players.Length; i++)
        {
            StartCoroutine(InitPrefab(data.players[i]));
        }
    }

    private IEnumerator InitPrefab(LBPlayerData playerData)
    {
        Instantiate(_recordPrefab, _recordAxis);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(playerData.photo);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            _recordPrefab.GetComponentInChildren<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        }
    }
}
