using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField]
    int _score;
    [SerializeField]
    string _playFabUserID;

    void Start()
    {
        InitializePlayFabAsync();
    }

    private async UniTask InitializePlayFabAsync()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = _playFabUserID,
            CreateAccount = true
        };

        LoginResult result = null;
        PlayFabError error = null;

        PlayFabClientAPI.LoginWithCustomID(request, x => result = x, x => error = x);
        await new WaitUntil(() => result != null || error != null);

        if (result != null)
        {
            Debug.Log("���O�C������");
        }
        else if (error != null)
        {
            Debug.Log("���O�C�����s");
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SubmitScore(_score);// �X�R�A���A�b�v����
        }
    }
    void SubmitScore(int score)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate
                    {
                        StatisticName = "HighScore",
                        Value = score
                    }
                }
            },
            result => Debug.Log("�X�R�A���M"),
            error => Debug.Log("�X�R�A���M���s")
            );
    }
}
