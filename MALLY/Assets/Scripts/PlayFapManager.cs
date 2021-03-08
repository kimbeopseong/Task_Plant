 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.EventsModels;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;

public class PlayFapManager : MonoBehaviourPunCallbacks
{
    public GameObject RegisterPanel, LoginPanel;

    [Header("Login")]
    public PlayerLeaderboardEntry MyPlayFabInfo;
    public List<PlayerLeaderboardEntry> PlayFabUserList = new List<PlayerLeaderboardEntry>();
    public InputField EmailInput, PasswordInput;
    
    [Header("Register")]
    public InputField RegisterEmail, RegisterPassword, RegisterNickname;

    bool isLoaded;

    #region PlayFab
    private void Awake() {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }
    #endregion

    #region Login
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
     private void OnLoginSuccess(LoginResult result)
    {
        // GetLeaderboard(result.PlayFabId);
        // PhotonNetwork.ConnectUsingSettings();
        print("로그인 성공!");
        SceneController.Instance.LoadScene(SceneNames.StartScene);
        
    }
    private void OnLoginFailure(PlayFabError error)
    {
        print("로그인 실패");
    }
    // void GetLeaderboard(string myId)
    // {
    //     PlayFabUserList.Clear();

    //     // 1000명 이하 = 10, 10000명 이하 = 100, ...
    //     for(int i = 0 ; i < 10 ; i++)
    //     {
    //         var request = new GetLeaderboardRequest { StartPosition = i * 100, StatisticName = "IDInfo", MaxResultsCount = 100, ProfileConstraints = new PlayerProfileViewConstraints() { ShowDisplayName = true } };
    //         PlayFabClientAPI.GetLeaderboard(request, (result) =>
    //         {
    //             if(result.Leaderboard.Count == 0) return;
    //             for(int j = 0 ; j < result.Leaderboard.Count ; j++)
    //             {
    //                 PlayFabUserList.Add(result.Leaderboard[j]);
    //                 if(result.Leaderboard[j].PlayFabId == myId) MyPlayFabInfo = result.Leaderboard[j];
    //             }
    //         },
    //         (error) => { });
    //     }
    // }
    public void GoRegister() 
    {
        ShowPanel(RegisterPanel);
    }
    #endregion

    #region Register
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Email = RegisterEmail.text, Password = RegisterPassword.text , Username = RegisterNickname.text, DisplayName = RegisterNickname.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result) 
    {
        print("회원가입 성공");
        GoLogin();
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        print("회원가입 실패");
    }
    public void GoLogin()
    {
        ShowPanel(LoginPanel);
    }
    #endregion

    void ShowPanel(GameObject CurPanel)
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(false);

        if(CurPanel) CurPanel.SetActive(true);
    }

}
