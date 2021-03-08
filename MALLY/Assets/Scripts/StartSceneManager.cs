using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.CloudScriptModels;
using PlayFab.ClientModels;

public class StartSceneManager : MonoBehaviour
{
    public GameObject FarmRegistPanel;

    [Header("FarmRegist")]
    public InputField FarmName, Serial;

    #region FarmRegist
    public void callRegist()
    {
        FarmRegistPanel.SetActive(true);
    }
    public void endRegist()
    {
        FarmName.text = "";
        Serial.text = "";
        FarmRegistPanel.SetActive(false);
    }
    public void createFarm()
    {
        if(Serial.text[0] == '1')
        {
            var newFarmRequest = new GrantCharacterToUserRequest {ItemId = "beginner", CharacterName = FarmName.text, CatalogVersion = "beginner"};
            PlayFabClientAPI.GrantCharacterToUser(newFarmRequest, OnCreateFarmSuccess, OnCreateFarmFailure);
        }
        else
        {
            print("serial 첫 숫자는 1입니다.");
        }
    }

    private void OnCreateFarmSuccess(GrantCharacterToUserResult result)
    {
        print("농장 생성 완료! 농장 이름: " + FarmName.text);
        endRegist();
    }
    private void OnCreateFarmFailure(PlayFabError error)
    {
        print("농장 생성 실패!" + error.ToString());
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
